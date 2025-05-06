using System.IO.Compression;
using CareLeavers.Web.Caching;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Contentful;
using Contentful.Core;
using Joonasw.AspNetCore.SecurityHeaders.Csp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebMarkupMin.AspNet.Common.Compressors;
using WebMarkupMin.AspNetCoreLatest;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Integration.Tests.TestSupport;

public class IntegrationTestWebFactory : WebApplicationFactory<Program>
{
    public FakeMessageHandler FakeMessageHandler { get; set; } = new();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTest");
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            var descriptorsToRemove = services.Where(d =>
                    d.ServiceType == typeof(IContentfulClient) ||
                    d.ServiceType == typeof(ICspNonceService) ||
                    d.ServiceType == typeof(IDistributedCache) ||
                    d.ServiceType == typeof(IFusionCache) ||
                    d.ServiceType == typeof(IOptions<ScriptOptions>) ||
                    d.ServiceType == typeof(IContentfulConfiguration))
                .ToList();

            descriptorsToRemove.ForEach(x => services.Remove(x));

            var httpClient = new HttpClient(FakeMessageHandler);
            var contentfulClient = new ContentfulClient(httpClient, "test", "test", "test");
            contentfulClient.SerializerSettings.Converters.RemoveAt(0);
            contentfulClient.SerializerSettings.Converters.Insert(0, new GDSAssetJsonConverter());
            contentfulClient.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            contentfulClient.SerializerSettings.ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            services.AddSingleton<IContentfulClient>(x => contentfulClient);

            services.AddSingleton<IDistributedCache, CacheDisabledDistributedCache>();

            services.AddFusionCache()
                .WithDefaultEntryOptions(o =>
                {
                    o.SkipMemoryCacheWrite = true;
                    o.SkipDistributedCacheWrite = true;
                });

            services.AddSingleton<ICspNonceService, MockCspNonceService>();

            services.AddScoped<IOptions<ScriptOptions>>(x => Options.Create(new ScriptOptions
            {
                Clarity = "test-clarity",
                GTM = "GTM-TEST",
                ShareaholicSiteId = "abcdefghijk",
                ShareaholicAppId = "12345678",
                ShowCookieBanner = false,
                AddCssVersion = false
            }));

            services.AddSingleton<IContentfulConfiguration, MockContentfulConfiguration>();

            // Disable minification for our testing
            services
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

        });
    }

}