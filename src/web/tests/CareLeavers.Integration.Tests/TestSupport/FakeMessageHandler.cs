using System.Net;
using CareLeavers.Web.Models.Content;
using Microsoft.AspNetCore.WebUtilities;
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
            var query = QueryHelpers.ParseQuery(request.RequestUri.Query);

            var matchingContent = Content.FirstOrDefault(c => c.ContentType == ContentfulConfigurationEntity.ContentType);
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