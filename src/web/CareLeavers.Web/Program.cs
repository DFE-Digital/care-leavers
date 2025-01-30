using System.Diagnostics.CodeAnalysis;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using CareLeavers.Web;
using CareLeavers.Web.Caching;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.ContentfulRenderers;
using CareLeavers.Web.Telemetry;
using Contentful.AspNetCore;
using Contentful.Core;
using Contentful.Core.Models;
using GovUk.Frontend.AspNetCore;
using Joonasw.AspNetCore.SecurityHeaders;
using Microsoft.Extensions.Caching.Distributed;
using OpenTelemetry.Trace;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .ConfigureLogging(Environment.GetEnvironmentVariable("ApplicationInsights__ConnectionString"))
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddControllersWithViews();
    builder.Services.AddGovUkFrontend();
    builder.Services.AddCsp(nonceByteAmount: 32);
    builder.Services.AddHsts(options =>
    {
        options.MaxAge = TimeSpan.FromDays(365);
        options.IncludeSubDomains = true;
        options.Preload = true;
    });

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

    builder.Services.AddHttpContextAccessor();
    
    builder.Services.AddContentful(builder.Configuration);

    builder.Services.AddHealthChecks();

    builder.Services.AddTransient<HtmlRenderer>((c) =>
    {
        var renderer = new HtmlRenderer(new HtmlRendererOptions
        {
            ListItemOptions = new ListItemContentRendererOptions
            {
                OmitParagraphTagsInsideListItems = true
            }
        });

        // Add custom GDS renderer
        renderer.AddRenderer(new GDSParagraphRenderer(renderer.Renderers));
        renderer.AddRenderer(new GDSHeaderRenderer(renderer.Renderers));

        return renderer;
    });

    builder.Services.Configure<CachingOptions>(
        builder.Configuration.GetSection(CachingOptions.Name)
    );

    builder.Services.AddOptions<CachingOptions>().BindConfiguration("Caching");
    var cachingOptions = builder.Configuration.GetSection("Caching").Get<CachingOptions>();

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

    var app = builder.Build();

    var contentfulClient = app.Services.GetRequiredService<IContentfulClient>();

    Constants.Serializer = contentfulClient.Serializer;
    Constants.SerializerSettings = contentfulClient.SerializerSettings;

// Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();
    
    app.MapHealthChecks("/health");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // add headers
    app.Use(async (context, next) =>
    {
        context.Response.Headers.Append("X-Frame-Options", "DENY");
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
        await next();
    });

    app.UseHsts();
    
    app.UseCsp(x =>
    {
        x.ByDefaultAllow.FromSelf();

        x.AllowScripts
            .FromSelf()
            .From("https://www.googletagmanager.com")
            .AddNonce();

        x.AllowStyles
            .From("https://rsms.me")
            .FromSelf()
            .AddNonce();

        x.AllowFonts
            .FromSelf()
            .From("https://rsms.me");
        
        x.AllowFrames
            .From("https://www.googletagmanager.com");
    });

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