using System.Net;

namespace CareLeavers.Integration.Tests.TestSupport;

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