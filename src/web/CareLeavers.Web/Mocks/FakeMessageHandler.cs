using System.Net;
using System.Web;

namespace CareLeavers.Web.Mocks;

public class FakeMessageHandler : HttpClientHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var queryVars = HttpUtility.ParseQueryString(request.RequestUri?.Query ?? string.Empty);
        var contentType = queryVars.Get("content_type");
        var slug = queryVars.Get("fields.slug");
        
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mocks", "Json", $"{slug}.json");
        
        var requestWrapperPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mocks", "Json", "RequestWrapper.json");
        
        var responseJson = await File.ReadAllTextAsync(requestWrapperPath, cancellationToken);
        
        var json = await File.ReadAllTextAsync(path, cancellationToken);

        responseJson = responseJson.Replace("**REPLACE**", json);
        
        return await Task.FromResult(new HttpResponseMessage
        {
            Content = new StringContent(responseJson),
            StatusCode = HttpStatusCode.OK
        });
    }
}