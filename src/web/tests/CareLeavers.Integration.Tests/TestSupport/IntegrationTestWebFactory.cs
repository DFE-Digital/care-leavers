using CareLeavers.Web.Caching;
using Contentful.Core;
using Joonasw.AspNetCore.SecurityHeaders.Csp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

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
                d.ServiceType == typeof(IContentfulClient) || d.ServiceType == typeof(ICspNonceService))
                .ToList();

            descriptorsToRemove.ForEach(x => services.Remove(x));

            var httpClient = new HttpClient(FakeMessageHandler);
            
            services.AddScoped<IContentfulClient>(x =>
                new ContentfulClient(httpClient, "test", "test", "test"));
            
            services.AddSingleton<IDistributedCache, CacheDisabledDistributedCache>();

            services.AddSingleton<ICspNonceService, MockCspNonceService>();
        });
    }

}