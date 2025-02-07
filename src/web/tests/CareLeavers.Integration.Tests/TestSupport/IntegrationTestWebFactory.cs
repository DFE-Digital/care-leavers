using CareLeavers.Web.Caching;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Contentful;
using Joonasw.AspNetCore.SecurityHeaders.Csp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace CareLeavers.Integration.Tests.TestSupport;

public class IntegrationTestWebFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTest");
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            var descriptorsToRemove = services.Where(d =>
                    d.ServiceType == typeof(IContentService) ||
                    d.ServiceType == typeof(ICspNonceService) ||
                    d.ServiceType == typeof(IDistributedCache) ||
                    d.ServiceType == typeof(IContentfulConfiguration))
                .ToList();

            descriptorsToRemove.ForEach(x => services.Remove(x));
            
            services.AddScoped<IContentService, MockContentService>();
            
            services.AddSingleton<IDistributedCache, CacheDisabledDistributedCache>();

            services.AddSingleton<ICspNonceService, MockCspNonceService>();

            services.AddSingleton<IContentfulConfiguration, MockContentfulConfiguration>();
        });
    }

}