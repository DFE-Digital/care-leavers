using System.Net;

namespace CareLeavers.Web.Tests;

public class MockHttpMessageHandler : HttpMessageHandler
{
    public HttpStatusCode StatusCode { private get; set; }
    public string Content { private get; set; } = null!;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HttpResponseMessage
        {
            StatusCode = StatusCode,
            Content = new StringContent(Content)
        });
    }
}