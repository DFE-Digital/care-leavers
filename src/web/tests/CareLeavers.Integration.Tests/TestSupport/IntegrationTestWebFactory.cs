using Contentful.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
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
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IContentfulClient));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var httpClient = new HttpClient(FakeMessageHandler);
            
            services.AddScoped<IContentfulClient>(x =>
                new ContentfulClient(httpClient, "test", "test", "test"));
        });
    }

}