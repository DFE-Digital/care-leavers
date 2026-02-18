using System.Net;
using CareLeavers.Integration.Tests.Tests;
using CareLeavers.Web;
using CareLeavers.Web.Models.Content;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using ContentfulContent = CareLeavers.Integration.Tests.Tests.ContentfulContent;

namespace CareLeavers.Integration.Tests.TestSupport;

public class FakeMessageHandler : HttpClientHandler
{
    public List<ContentfulContent> Content { get; set; } = [];
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = string.Empty;

        // If We only have a single piece of content, return that
        if (Content.Count == 1)
        {
            response = Content.First().Content;
        }
        // If we're requesting a page, just grab the first page
        else if (request.RequestUri != null && request.RequestUri.PathAndQuery.Contains("content_type=page"))
        {
            var query = QueryHelpers.ParseQuery(request.RequestUri.Query);

            var matchingContent = Content.FirstOrDefault(c => c.ContentType == Page.ContentType);
            if (matchingContent != null)
            {
                response = matchingContent.Content;
            }
        } 
        // If we're requesting config, just grab the first config
        else if (request.RequestUri != null && request.RequestUri.PathAndQuery.Contains("content_type=configuration"))
        {
            var configuration = new MockContentfulConfiguration().GetConfiguration().Result;

            var json = JsonConvert.SerializeObject(configuration, Constants.SerializerSettings);
            
            var wrapper = await File.ReadAllTextAsync(Path.Combine(WebFixture.WrapperBasePath, "RequestWrapper.json"), cancellationToken);
     
            response = wrapper.Replace("**REPLACE**", json);
        }
        // Same for redirection rules
        else if (request.RequestUri != null && request.RequestUri.PathAndQuery.Contains("content_type=redirectionRule"))
        {
            var matchingContent = Content.FirstOrDefault(c => c.ContentType == RedirectionRules.ContentType);
            if (matchingContent != null)
            {
                response = matchingContent.Content;
            }
        } 
        // Otherwise, if we've got an ID, go grab the first matching one with an ID
        else if (request.RequestUri != null && request.RequestUri.PathAndQuery.Contains("id="))
        {
            var query = QueryHelpers.ParseQuery(request.RequestUri.Query);

            var matchingContent = Content.FirstOrDefault(c => c.Id == query.GetValueOrDefault("sys.id"));
            if (matchingContent != null)
            {
                response = matchingContent.Content;
            }
        }

        return await Task.FromResult(new HttpResponseMessage
        {
            Content = new StringContent(response),
            StatusCode = HttpStatusCode.OK
        });
    }
}