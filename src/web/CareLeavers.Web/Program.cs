using System.Diagnostics.CodeAnalysis;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using CareLeavers.Web;
using CareLeavers.Web.Caching;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Contentful;
using CareLeavers.Web.Contentful.Webhooks;
using CareLeavers.Web.ContentfulRenderers;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Telemetry;
using CareLeavers.Web.Translation;
using Contentful.AspNetCore;
using Contentful.AspNetCore.MiddleWare;
using Contentful.Core;
using Contentful.Core.Models;
using Joonasw.AspNetCore.SecurityHeaders;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenTelemetry.Trace;
using Serilog;
using DistributedCacheExtensions = CareLeavers.Web.Caching.DistributedCacheExtensions;

Log.Logger = new LoggerConfiguration()
    .ConfigureLogging(Environment.GetEnvironmentVariable("ApplicationInsights__ConnectionString"))
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    #region Additional Logging and Application Insights
    
    Log.Logger.Information("Starting application");
    Log.Logger.Information("Environment: {Environment}", builder.Environment.EnvironmentName);
    
    builder.Services.AddSerilog((_, lc) => lc
        .ConfigureLogging(builder.Configuration["ApplicationInsights:ConnectionString"]));

    var appInsightsConnectionString = builder.Configuration.GetValue<string>("ApplicationInsights:ConnectionString");

    if (!string.IsNullOrEmpty(appInsightsConnectionString))
    {
        builder.Services.AddOpenTelemetry()
            .WithTracing(x =>
            {
                x.AddAspNetCoreInstrumentation();
                x.AddProcessor<RouteTelemetryProcessor>();
            })
            .UseAzureMonitor(x => x.ConnectionString = appInsightsConnectionString);
    }

    #endregion
    
    #region Controllers
    
    builder.Services.AddControllersWithViews();
    
    #endregion
    
    #region Setup security and headers
    
    builder.Services.AddCsp(nonceByteAmount: 32);
    builder.Services.AddHsts(options =>
    {
        options.MaxAge = TimeSpan.FromDays(365);
        options.IncludeSubDomains = true;
        options.Preload = true;
    });

    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = _ => true;
        options.MinimumSameSitePolicy = SameSiteMode.Strict;
    });
    
    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedFor;
        options.KnownProxies.Clear();
        options.KnownNetworks.Clear();
        options.AllowedHosts = new List<string>
        {
            "*.azurewebsites.net",
            "*.azurefd.net",
            "*.support-for-care-leavers.education.gov.uk"
        };
    });
        
    #endregion
    
    #region Contentful
    
    builder.Services.AddScoped<IContentService, ContentfulContentService>();
    builder.Services.AddContentful(builder.Configuration);
    builder.Services.AddScoped<IContentfulConfiguration, ContentfulConfiguration>();
    
    builder.Services.AddTransient<HtmlRenderer>(serviceProvider =>
    {
        // Turn off paragraph tags inside Contentful paragraph list items
        var renderer = new HtmlRenderer(new HtmlRendererOptions
        {
            ListItemOptions = new ListItemContentRendererOptions
            {
                OmitParagraphTagsInsideListItems = true
            }
        });

        // Add custom renderers
        renderer.AddRenderer(new GDSParagraphRenderer(renderer.Renderers));
        renderer.AddRenderer(new GDSHeaderRenderer(renderer.Renderers));
        renderer.AddRenderer(new GDSAssetRenderer(renderer.Renderers));
        renderer.AddRenderer(new GDSLinkRenderer(renderer.Renderers));
        renderer.AddRenderer(new GDSListRenderer(renderer.Renderers));
        renderer.AddRenderer(new GDSHorizontalRulerContentRenderer());
        renderer.AddRenderer(new GDSDefinitionLinkRenderer());
        renderer.AddRenderer(new GDSSpacerRenderer());
        renderer.AddRenderer(new GDSGridRenderer(serviceProvider));
        renderer.AddRenderer(new GDSRichContentRenderer(serviceProvider));
        renderer.AddRenderer(new GDSStatusCheckerRenderer(serviceProvider));
        renderer.AddRenderer(new GDSRiddleRenderer(serviceProvider));
        renderer.AddRenderer(new GDSBannerRenderer(serviceProvider));
        renderer.AddRenderer(new GDSDefinitionRenderer(serviceProvider));

        return renderer;
    });
    
    #endregion
    
    #region Configuration

    builder.Services.AddOptions<ScriptOptions>().BindConfiguration(ScriptOptions.Name);
    builder.Services.AddOptions<CachingOptions>().BindConfiguration(CachingOptions.Name);

    builder.Services.AddOptions<AzureTranslationOptions>().BindConfiguration(AzureTranslationOptions.Name);

    if (string.IsNullOrEmpty(builder.Configuration.GetValue<string>("AzureTranslation:AccessKey")))
    {
        Log.Logger.Information("Azure Translation subscription key not found, translation service will be disabled");
        builder.Services.AddSingleton<ITranslationService, NoTranslationService>();
    }
    else
    {
        builder.Services.AddScoped<ITranslationService, AzureTranslationService>();
    }

    #endregion
    
    #region Distributed Caching
    
    var cachingOptions = builder.Configuration.GetSection(CachingOptions.Name).Get<CachingOptions>();

    if (cachingOptions?.Type == "Memory")
    {
        builder.Services.AddDistributedMemoryCache();
    }
    else if (cachingOptions?.Type == "Redis")
    {
        builder.Services.AddStackExchangeRedisCache(x =>
        {
            x.Configuration = cachingOptions.ConnectionString;
        });
    }
    else
    {
        builder.Services.AddSingleton<IDistributedCache, CacheDisabledDistributedCache>();
    }

    DistributedCacheExtensions.DefaultCacheOptions.SetAbsoluteExpiration(
        cachingOptions?.Duration ?? TimeSpan.FromDays(30));
    
    #endregion
    
    #region HTTP Context and Healthchecks
    
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddHealthChecks();
    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
    });
    
    #endregion
    
    var app = builder.Build();
    
    #region Contentful Setup

    var contentfulClient = app.Services.GetRequiredService<IContentfulClient>();
    contentfulClient.SerializerSettings.Converters.RemoveAt(0);
    contentfulClient.SerializerSettings.Converters.Insert(0, new GDSAssetJsonConverter());
    contentfulClient.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    contentfulClient.SerializerSettings.Formatting = Formatting.Indented;
    contentfulClient.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    contentfulClient.SerializerSettings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;
    contentfulClient.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
    contentfulClient.SerializerSettings.ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy()
        
    };
    contentfulClient.SerializerSettings.MaxDepth = 128;
    contentfulClient.Serializer.MaxDepth = 128;

    Constants.Serializer = contentfulClient.Serializer;
    Constants.SerializerSettings = contentfulClient.SerializerSettings;

    app.UseContentfulWebhooks(consumers =>
    {
        consumers.AddConsumer<Entry<ContentfulContent>>("*", "Entry", "*",  async entry =>
        {
            var webhookConsumer = new PublishContentfulWebhook(
                app.Services.GetRequiredService<IContentfulClient>(),
                app.Services.GetRequiredService<IDistributedCache>(),
                app.Services.GetRequiredService<IContentfulManagementClient>(),
                app.Services.GetRequiredService<ILogger<PublishContentfulWebhook>>());

            await webhookConsumer.Consume(entry);
            
            return new { Result = "OK" };
        });

        consumers.AddConsumer<Asset>("*", "Asset", "*", async asset =>
        {
            var webhookConsumer = new PublishAssetWebhook(
                app.Services.GetRequiredService<IContentfulClient>(),
                app.Services.GetRequiredService<IDistributedCache>(),
                app.Services.GetRequiredService<ILogger<PublishAssetWebhook>>());

            await webhookConsumer.Consume(asset);

            return new { Result = "OK" };
        });
    });

    #endregion
    
    #region Setup error pages
    
    app.UseStatusCodePagesWithReExecute("/en/error", "?statusCode={0}");

    
    if (!app.Environment.IsDevelopment())
    {
        // If we're not in development mode, use the error handler page
        app.UseExceptionHandler("/en/error");
    }
    
    // Redirect 404 responses to the page not found page
    app.Use(async (context, next) =>
    {
        await next();

        if (context.Response is { StatusCode: 404, HasStarted: false })
        {
            // Log the error or handle it accordingly
            context.Request.Path = "/en/page-not-found"; // Redirect to a custom not found page
            await next();
        }
    });
    
    #endregion

    #region Default setup, logging, HTTPS, static files, routing etc
    
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    
    var cacheMaxAgeOneWeek = (60 * 60 * 24 * 7).ToString();
    app.UseStaticFiles(new StaticFileOptions()
    {
        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append(
                "Cache-Control", $"public, max-age={cacheMaxAgeOneWeek}");
        }
    });
    app.UseResponseCompression();
    app.UseRouting();
    app.UseAuthorization();
    app.MapHealthChecks("/health");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Contentful}/{action=Homepage}");

    #endregion
    
    #region Security and Cross-Site-Scripting protection

    app.UseCookiePolicy();
    app.UseForwardedHeaders();
    
    // add headers
    app.Use(async (context, next) =>
    {
        context.Response.Headers.Append("X-Frame-Options", "DENY");
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
        await next();
    });
    
    app.UseCsp(x =>
    {
        x.ByDefaultAllow.FromNowhere();

        var config = app.Configuration.GetSection("Csp").Get<CspConfiguration>() ?? new CspConfiguration();

        x.AllowScripts
            .FromSelf()
            .AddNonce();

        config.AllowScriptUrls.ForEach(f => x.AllowScripts.From(f));
        config.AllowHashes.ForEach(f => x.AllowScripts.WithHash(f));

        x.AllowStyles
            .FromSelf()
            .AllowUnsafeInline();

        config.AllowStyleUrls.ForEach(f => x.AllowStyles.From(f));

        x.AllowFonts
            .FromSelf()
            .From("data:");

        config.AllowFontUrls.ForEach(f => x.AllowFonts.From(f));
        
        x.AllowFraming.FromSelf(); // Block framing on other sites, equivalent to X-Frame-Options: DENY
        config.AllowFrameUrls.ForEach(f => x.AllowFraming.From(f));
        config.AllowFrameUrls.ForEach(f => x.AllowFrames.From(f));

        x.AllowFormActions.ToSelf();

        x.AllowImages
            .FromSelf()
            .From("data:");
        
        config.AllowImageUrls.ForEach(f => x.AllowImages.From(f));

        x.AllowConnections
            .ToSelf();
        
        config.AllowConnectUrls.ForEach(f => x.AllowConnections.To(f));
        
        if (config.ReportOnly)
        {
            x.SetReportOnly();
        }

    });
    
    #endregion

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    await Log.CloseAndFlushAsync();
}

[ExcludeFromCodeCoverage]
public partial class Program
{
    protected Program() { }
}