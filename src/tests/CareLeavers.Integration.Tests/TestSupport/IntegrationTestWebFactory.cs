using System.Net;
using Contentful.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace CareLeavers.Integration.Tests.TestSupport;

public class IntegrationTestWebFactory : WebApplicationFactory<Program>
{
    //public IContentfulClient ContentfulClientMock { get; set; } = Substitute.For<IContentfulClient>();

    public FakeMessageHandler FakeContentfulHttpClient { get; set; } = new();
    
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


            var httpClient = new HttpClient(FakeContentfulHttpClient);
            
            services.AddScoped<IContentfulClient>(x =>
                new ContentfulClient(httpClient, "test", "test", "test"));
        });
    }

}

public class FakeMessageHandler : HttpClientHandler
{
    public string Response { get; set; } = string.Empty;
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new HttpResponseMessage
        {
            Content = new StringContent(Response),
            StatusCode = HttpStatusCode.OK
        });
    }
}