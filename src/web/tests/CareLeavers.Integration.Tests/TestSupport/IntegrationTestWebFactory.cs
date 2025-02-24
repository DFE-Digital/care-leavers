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

            services.AddSingleton<ICspNonceService, MockCspNonceService>();

            services.AddScoped<IOptions<ScriptOptions>>(x => Options.Create(new ScriptOptions
            {
                Clarity = "test-clarity",
                GTM = "GTM-TEST",
                ShareThis = "abcdefghijk",
                ShowCookieBanner = false,
                AddCssVersion = false
            }));

            services.AddSingleton<IContentfulConfiguration, MockContentfulConfiguration>();
        });
    }

}