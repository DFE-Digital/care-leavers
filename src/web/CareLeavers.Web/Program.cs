using System.Diagnostics.CodeAnalysis;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using CareLeavers.Web;
using CareLeavers.Web.Caching;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Contentful;
using CareLeavers.Web.ContentfulRenderers;
using CareLeavers.Web.Mocks;
using CareLeavers.Web.Telemetry;
using Contentful.AspNetCore;
using Contentful.Core;
using Contentful.Core.Models;
using GovUk.Frontend.AspNetCore;
using Joonasw.AspNetCore.SecurityHeaders;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenTelemetry.Trace;
using Serilog;

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
    
    #region GOV.UK front end
    
    builder.Services.AddGovUkFrontend();
    
    #endregion
    
    #region Setup security and headers
    
    builder.Services.AddCsp(nonceByteAmount: 32);
    builder.Services.AddHsts(options =>
    {
        options.MaxAge = TimeSpan.FromDays(365);
        options.IncludeSubDomains = true;
        options.Preload = true;
    });
    
    #endregion
    
    #region Contentful
    
    builder.Services.AddScoped<IContentService, ContentfulContentService>();
    if (!builder.Environment.IsEnvironment("EndToEnd"))
    {
        builder.Services.AddContentful(builder.Configuration);
        builder.Services.AddScoped<IContentfulConfiguration, ContentfulConfiguration>();
    }
    else
    {
        var httpClient = new HttpClient(new FakeMessageHandler());
        var mockedContentfulClient = new ContentfulClient(httpClient, "test", "test", "test");
        mockedContentfulClient.ContentTypeResolver = new ContentfulEntityResolver();
        mockedContentfulClient.SerializerSettings.Converters.RemoveAt(0);
        mockedContentfulClient.SerializerSettings.Converters.Insert(0, new GDSAssetJsonConverter());
        mockedContentfulClient.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        mockedContentfulClient.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };
        
        builder.Services.AddSingleton<IContentfulClient>(x => mockedContentfulClient);
        builder.Services.AddScoped<IContentfulConfiguration, MockedContentfulConfiguration>();
    }
    
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
        renderer.AddRenderer(new GDSGridRenderer(serviceProvider));
        renderer.AddRenderer(new GDSHorizontalRulerContentRenderer());
        renderer.AddRenderer(new GDSRichContentRenderer(serviceProvider));
        renderer.AddRenderer(new GDSEntityLinkContentRenderer(renderer.Renderers));
        renderer.AddRenderer(new GDSStatusCheckerRenderer(serviceProvider));
        renderer.AddRenderer(new GDSRiddleRenderer(serviceProvider));

        return renderer;
    });
    
    #endregion
    
    #region Configuration

    builder.Services.AddOptions<ScriptOptions>().BindConfiguration(ScriptOptions.Name);
    builder.Services.AddOptions<CachingOptions>().BindConfiguration(CachingOptions.Name);
    
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
    
    #endregion
    
    #region HTTP Context and Healthchecks
    
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddHealthChecks();
    
    #endregion
    
    var app = builder.Build();
    
    #region Contentful Setup

    var contentfulClient = app.Services.GetRequiredService<IContentfulClient>();
    contentfulClient.SerializerSettings.Converters.RemoveAt(0);
    contentfulClient.SerializerSettings.Converters.Insert(0, new GDSAssetJsonConverter());
    contentfulClient.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    contentfulClient.SerializerSettings.Formatting = Formatting.Indented;
    contentfulClient.SerializerSettings.ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    };

    Constants.Serializer = contentfulClient.Serializer;
    Constants.SerializerSettings = contentfulClient.SerializerSettings;

    #endregion
    
    #region Setup error pages and HSTS
    
    if (!app.Environment.IsDevelopment())
    {
        // TODO: Setup views for exceptions
        app.UseExceptionHandler("/Home/Error");
        
        // TODO: Setup view for 404 not found
        
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    
    #endregion

    #region Default setup, logging, HTTPS, static files, routing etc
    
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();
    app.MapHealthChecks("/health");

    #endregion
    
    #region Route Mapping
    
    // Sitemap
    app.MapControllerRoute(
        "Sitemap",
        "sitemap.xml",
        new { controller = "Sitemap", action = "SiteMap" }
    );
    
    // JSON
    app.MapControllerRoute(
        "JSON",
        "/json/{**slug}",
        new { controller = "Contentful", action = "ContentJson" }
    );
    
    // Homepage
    app.MapControllerRoute(
        name: "Homepage",
        pattern: "/",        
        new { controller = "Contentful", action = "Homepage" }

    );
    
    // Content
    app.MapControllerRoute(
        "default",
        "/{**slug}",
        new { controller = "Contentful", action = "Content" }
    );
   
    
    #endregion
    
    #region Security and Cross-Site-Scripting protection
    
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

        x.AllowStyles
            .FromSelf()
            .AllowUnsafeInline();

        config.AllowStyleUrls.ForEach(f => x.AllowStyles.From(f));

        x.AllowFonts
            .FromSelf()
            .From("data:");

        config.AllowFontUrls.ForEach(f => x.AllowFonts.From(f));
        
        x.AllowFraming.FromNowhere(); // Block framing on other sites, equivalent to X-Frame-Options: DENY

        x.AllowFormActions.ToSelf();

        config.AllowFrameUrls.ForEach(f => x.AllowFrames.From(f));

        x.AllowImages
            .FromSelf()
            .From("data:");
        
        config.AllowImageUrls.ForEach(f => x.AllowImages.From(f));

        x.AllowConnections
            .ToSelf();
        
        config.AllowConnectUrls.ForEach(f => x.AllowConnections.To(f));

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