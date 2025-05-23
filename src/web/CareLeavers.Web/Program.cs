using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using CareLeavers.Web;
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
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.HttpOverrides;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Serilog;
using WebMarkupMin.AspNet.Common.Compressors;
using WebMarkupMin.AspNetCoreLatest;
using WebMarkupMin.Core;
using ZiggyCreatures.Caching.Fusion;
using static System.TimeSpan;

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
            .WithTracing(tracing => tracing
                .AddAspNetCoreInstrumentation()
                .AddProcessor<RouteTelemetryProcessor>()
                .AddFusionCacheInstrumentation()
            )
            .WithMetrics(metrics => metrics
                .AddAspNetCoreInstrumentation()
                .AddFusionCacheInstrumentation()
                )
            .UseAzureMonitor(monitor => monitor.ConnectionString = appInsightsConnectionString);
    }

    #endregion

    #region Minification

    builder.Services
        .AddWebMarkupMin(options =>
        {
            options.DisablePoweredByHttpHeaders = true;
        })
        .AddHtmlMinification()
        .AddXhtmlMinification()
        .AddXmlMinification()
        .AddHttpCompression(options =>
        {
            options.CompressorFactories = new List<ICompressorFactory>
            {
                new BuiltInBrotliCompressorFactory(new BuiltInBrotliCompressionSettings
                {
                    Level = CompressionLevel.Fastest
                }),
                new DeflateCompressorFactory(new DeflateCompressionSettings
                {
                    Level = CompressionLevel.Fastest
                }),
                new GZipCompressorFactory(new GZipCompressionSettings
                {
                    Level = CompressionLevel.Fastest
                })
            };
        });

    #endregion
    
    #region Controllers
    
    builder.Services.AddControllersWithViews();
    
    #endregion
    
    #region Setup security and headers
    
    builder.Services.AddCsp(nonceByteAmount: 32);
    builder.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = _ => true;
        options.MinimumSameSitePolicy = SameSiteMode.Strict;
        options.Secure = CookieSecurePolicy.Always;
        options.ConsentCookie.IsEssential = true;
        options.HttpOnly = HttpOnlyPolicy.None;
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
    
    #region Contentful and Renderers
    
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

        // Add custom renderers with no renderers or DI
        renderer.AddRenderer(new GDSHorizontalRulerContentRenderer());
        renderer.AddRenderer(new GDSSpacerRenderer());
        
        // Add custom renderers, passing renderer collection
        renderer.AddRenderer(new GDSParagraphRenderer(renderer.Renderers));
        renderer.AddRenderer(new GDSHeaderRenderer(renderer.Renderers));
        renderer.AddRenderer(new GDSAssetRenderer(renderer.Renderers));
        renderer.AddRenderer(new GDSListRenderer(renderer.Renderers));
        
        // Add custom renderers with DI
        renderer.AddRenderer(new GDSDefinitionLinkRenderer(serviceProvider));
        renderer.AddRenderer(new GDSGridRenderer(serviceProvider));
        renderer.AddRenderer(new GDSRichContentRenderer(serviceProvider));
        renderer.AddRenderer(new GDSStatusCheckerRenderer(serviceProvider));
        renderer.AddRenderer(new GDSRiddleRenderer(serviceProvider));
        renderer.AddRenderer(new GDSBannerRenderer(serviceProvider));
        renderer.AddRenderer(new GDSDefinitionRenderer(serviceProvider));
        renderer.AddRenderer(new GDSCallToActionRenderer(serviceProvider));
        renderer.AddRenderer(new GDSButtonRenderer(serviceProvider));
        
        // Add custom renderers with renderers and DI
        renderer.AddRenderer(new GDSLinkRenderer(renderer.Renderers, serviceProvider));

        return renderer;
    });
    
    #endregion
    
    #region Configuration

    builder.Services.AddOptions<ScriptOptions>().BindConfiguration(ScriptOptions.Name);
    builder.Services.AddOptions<CachingOptions>().BindConfiguration(CachingOptions.Name);
    builder.Services.AddOptions<PdfGenerationOptions>().BindConfiguration(PdfGenerationOptions.Name);
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

    builder.Services.AddFusionCacheNewtonsoftJsonSerializer(new JsonSerializerSettings()
    {
        Converters = [new GDSAssetJsonConverter()],
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        Formatting = Formatting.None,
        NullValueHandling = NullValueHandling.Ignore,
        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
        TypeNameHandling = TypeNameHandling.Auto,
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        },
        MaxDepth = 128
    });
    

    switch (cachingOptions?.Type)
    {
        case "Memory":
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddFusionCacheMemoryBackplane();
            builder.Services.AddFusionCache()
                .WithRegisteredSerializer()
                .WithDefaultEntryOptions(new FusionCacheEntryOptions()
                {
                    Duration = cachingOptions?.Duration ?? FromDays(30),
                    SkipBackplaneNotifications = true
                });
            break;
        case "Redis":
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cachingOptions.ConnectionString;
            });
        
            builder.Services.AddFusionCache()
                .WithOptions(opt =>
                {
                    opt.CacheKeyPrefix = "";
                })
                .WithRegisteredSerializer()
                .WithRegisteredDistributedCache()
                .WithStackExchangeRedisBackplane(x => x.Configuration = cachingOptions.ConnectionString )
                .WithDefaultEntryOptions(new FusionCacheEntryOptions()
                {
                    Duration = cachingOptions?.Duration ?? FromDays(30),
                    DistributedCacheDuration = cachingOptions?.Duration ?? FromDays(30)
                });
            break;
        default:
            builder.Services.AddFusionCache()
                .WithoutDistributedCache()
                .WithoutBackplane()
                .WithDefaultEntryOptions(new FusionCacheEntryOptions()
                {
                    Duration = Zero
                });
            break;
    }
    
    #endregion
    
    #region HTTP Context and Healthchecks
    
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddHealthChecks();
    
    #endregion
    
    var app = builder.Build();
    
    #region Content Security (CSP) and Headers

    // HSTS
    app.UseStrictTransportSecurity(new HstsOptions(FromDays(365), true, true));

    // Security Headers
    app.Use(async (context, next) =>
    {
        context.Response.Headers.Append("X-Frame-Options", "DENY");
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
        await next();
    });
    
    // Cookie Security
    app.UseCookiePolicy();
    
    // Content Security Policy
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

        x.AllowManifest
            .FromSelf();

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

    app.UseHttpsRedirection();
    app.UseForwardedHeaders();

    #endregion
    
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
                app.Services.GetRequiredService<IFusionCache>(),
                app.Services.GetRequiredService<IContentfulManagementClient>(),
                app.Services.GetRequiredService<ILogger<PublishContentfulWebhook>>());

            await webhookConsumer.Consume(entry);
            
            return new { Result = "OK" };
        });

        consumers.AddConsumer<Asset>("*", "Asset", "*", async asset =>
        {
            var webhookConsumer = new PublishAssetWebhook(
                app.Services.GetRequiredService<IContentfulClient>(),
                app.Services.GetRequiredService<IFusionCache>(),
                app.Services.GetRequiredService<ILogger<PublishAssetWebhook>>());

            await webhookConsumer.Consume(asset);

            return new { Result = "OK" };
        });
    });

    #endregion
    
    #region Rebrand

    SiteConfiguration.Rebrand = app.Configuration.GetValue<bool>("Rebrand") || DateTime.Today >= new DateTime(2025, 6, 25);

    #endregion

    #region Minification

    app.UseWebMarkupMin();

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
            if (context.Request.Path.Value != null && context.Request.Path.Value.Contains('.'))
            {
                // No point running the friendly 404 page if we're only looking for static files
                switch (context.Request.Path.Value.Substring(context.Request.Path.Value.LastIndexOf('.')).ToLower())
                {
                    // Images
                    case ".gif":
                    case ".jpg":
                    case ".png":
                    case ".ico":
                    case ".svg":
                    // HTML
                    case ".htm":
                    case ".html":
                    // CSS
                    case ".css":
                    // Javascript
                    case ".js":
                    // Other
                    case ".map":
                    case ".manifest":
                    case ".txt":
                        // Return a simple response    
                        context.Response.Clear();
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.Response.WriteAsync("Not Found");
                        return;
                    default:
                        // Redirect to the not found page
                        context.Request.Path = "/en/page-not-found";
                        await next();
                        break;
                }
            }
            else
            {
                // Redirect to the not found page
                context.Request.Path = "/en/page-not-found";
                await next();
            }
        }
    });
    
    #endregion

    #region Static files, routing, health checks, and default route

    app.UseStaticFiles(new StaticFileOptions()
    {
        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append(
                "Cache-Control", $"public, max-age={FromDays(31).TotalSeconds}");
        }
    });
    app.UseRouting();
    app.MapHealthChecks("/health");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Contentful}/{action=Homepage}");

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