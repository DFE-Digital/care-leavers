using System.Net.Http;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using Joonasw.AspNetCore.SecurityHeaders.Csp;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace CareLeavers.Web.GetToAnAnswerRun;

public class GetToAnAnswerRunClient(
    HttpClient httpClient, 
    IServiceProvider serviceProvider,
    ILogger<GetToAnAnswerRunClient> logger,
    IMemoryCache cache
) : IGetToAnAnswerRunClient {
    private const string NonceAttribute = "nonce";
    private readonly IConfiguration _configuration = serviceProvider.GetRequiredService<IConfiguration>();
    private readonly ICspNonceService _cspNonceService = serviceProvider.GetRequiredService<ICspNonceService>();
    private readonly IMemoryCache _cache = cache;

    public async Task<string> GetStartPageOrInitialState(string languageCode, string questionnaireSlug)
    {
        var cacheKey = $"gtaa:start:{languageCode}:{questionnaireSlug}";
        var endpoint = $"/questionnaires/{HttpUtility.UrlEncode(questionnaireSlug)}/start?embed=true";

        return await GetCachedQuestionnairePage(languageCode, questionnaireSlug, cacheKey, endpoint, cacheMinutes: null);
    }

    public async Task<string> GetInitialState(string languageCode, string questionnaireSlug)
    {
        var cacheKey = $"gtaa:next:{languageCode}:{questionnaireSlug}";
        var endpoint = $"/questionnaires/{HttpUtility.UrlEncode(questionnaireSlug)}/next?embed=true";

        // Cache for 5 minutes - next pages are typically static per questionnaire
        return await GetCachedQuestionnairePage(languageCode, questionnaireSlug, cacheKey, endpoint, cacheMinutes: 5);
    }

    public async Task<string> GetNextState(string thisOrigin, string languageCode, string questionnaireSlug, Dictionary<string, StringValues> formData)
    {
        // Flatten the dictionary for FormUrlEncodedContent
        var formContent = formData
            .SelectMany(kvp => kvp.Value, (kvp, value) => new KeyValuePair<string, string>(kvp.Key, value ?? string.Empty));

        var responseMessage = await httpClient.PostAsync(
            $"/questionnaires/{HttpUtility.UrlEncode(questionnaireSlug)}/next?embed=true", 
            new FormUrlEncodedContent(formContent));
        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to get next state for questionnaire {questionnaireSlug}");
        }
        var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
        var html = Encoding.UTF8.GetString(bytes);
        // Replace the base url with the local url so that the embedded content redirects to the correct page
        return SubstitutePageContent(languageCode, html, thisOrigin);
    }

    public async Task<(Stream fileStream, string contentType)> GetDecorativeImage(string questionnaireSlug)
    {
        var response = await httpClient.GetAsync(
            $"/questionnaires/{HttpUtility.UrlEncode(questionnaireSlug)}/decorative-image");
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to get decorative image for questionnaire {questionnaireSlug}");
        }
        var stream = await response.Content.ReadAsStreamAsync();
        var contentType = response.Content.Headers.ContentType?.MediaType ?? "image/png";
        return (stream, contentType);
    }

    private async Task<string> GetCachedQuestionnairePage(
        string languageCode, 
        string questionnaireSlug, 
        string cacheKey, 
        string endpoint, 
        int? cacheMinutes)
    {
        if (_cache.TryGetValue(cacheKey, out string? cachedHtml) && cachedHtml != null)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("Cache hit for questionnaire: {Slug}", questionnaireSlug);
            }
            return cachedHtml;
        }

        var responseMessage = await httpClient.GetAsync(endpoint);

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to get questionnaire page for {questionnaireSlug}");
        }

        var html = await ReadResponseAsString(responseMessage);
        var substituted = SubstitutePageContent(languageCode, html);

        // Cache if cacheMinutes is specified
        if (cacheMinutes.HasValue)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheMinutes.Value));
            _cache.Set(cacheKey, substituted, cacheOptions);
        }

        return substituted;
    }

    private static async Task<string> ReadResponseAsString(HttpResponseMessage responseMessage)
    {
        var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
        return Encoding.UTF8.GetString(bytes);
    }

    private string SubstitutePageContent(string languageCode, string html, string? thisOrigin = null)
    {
        var doc = new HtmlDocument();
        doc.OptionOutputAsXml = false;
        doc.OptionWriteEmptyNodes = true;
        doc.OptionDefaultStreamEncoding = Encoding.UTF8;
        doc.LoadHtml(html);
        // Inject nonce into script and style tags
        InjectBaseUrlAndNonce(languageCode, doc, thisOrigin);
        using var writer = new StringWriter();
        doc.Save(writer);
        return writer.ToString();
    }

    private void InjectBaseUrlAndNonce(string languageCode, HtmlDocument doc, string? thisOrigin = null)
    {
        var baseUrl = _configuration["GetToAnAnswer:BaseUrl"];
        var nonce = _cspNonceService.GetNonce();

        if (string.IsNullOrEmpty(baseUrl))
        {
            return;
        }

        // Add nonce to all script tags that don't already have one
        var scriptTags = doc.DocumentNode.SelectNodes("//script");
        if (scriptTags != null)
        {
            foreach (var script in scriptTags)
            {
                var nonceAttribute = script.Attributes[NonceAttribute];
                if (nonceAttribute == null || string.IsNullOrWhiteSpace(nonceAttribute.Value))
                {
                    script.SetAttributeValue(NonceAttribute, nonce);
                }

                if (script.Attributes.Contains("src"))
                {
                    var srcValue = script.Attributes["src"]?.Value;
                    if (!string.IsNullOrEmpty(srcValue) && srcValue.StartsWith('/'))
                    {
                        script.SetAttributeValue("src", baseUrl + srcValue);
                    }
                }
                script.Attributes.Remove("asp-add-nonce");
            }
        }

        // Add baseUrls to all link tags that don't already have one
        var linkTags = doc.DocumentNode.SelectNodes("//link");
        if (linkTags != null)
        {
            foreach (var link in linkTags)
            {
                if (link.Attributes.Contains("href"))
                {
                    var hrefValue = link.Attributes["href"]?.Value;
                    if (!string.IsNullOrEmpty(hrefValue) && hrefValue.StartsWith('/'))
                    {
                        link.SetAttributeValue("href", baseUrl + hrefValue);
                    }
                }
                link.Attributes.Remove("asp-add-nonce");
            }
        }

        // Add nonce to all style tags that don't already have one
        var styleTags = doc.DocumentNode.SelectNodes("//style");
        if (styleTags != null)
        {
            foreach (var style in styleTags)
            {
                var nonceAttribute = style.Attributes[NonceAttribute];
                if (nonceAttribute == null || string.IsNullOrWhiteSpace(nonceAttribute.Value))
                {
                    style.SetAttributeValue(NonceAttribute, nonce);
                }
                style.Attributes.Remove("asp-add-nonce");
            }
        }

        // Add nonce to all form tags that need questionnaire path replacement
        var formTags = doc.DocumentNode.SelectNodes("//form");
        if (formTags != null)
        {
            foreach (var form in formTags)
            {
                if (form.Attributes.Contains("action"))
                {
                    var actionValue = form.Attributes["action"]?.Value;
                    if (!string.IsNullOrEmpty(actionValue) && actionValue.StartsWith("/questionnaires/"))
                    {
                        form.SetAttributeValue("action", actionValue
                            .Replace("/questionnaires", $"/{languageCode}/get-to-an-answer-questionnaires"));
                    }
                }
            }
        }

        // Add nonce to all anchor tags that need questionnaire path replacement
        var anchorTags = doc.DocumentNode.SelectNodes("//a");
        if (anchorTags != null)
        {
            foreach (var anchor in anchorTags)
            {
                if (anchor.Attributes.Contains("href"))
                {
                    var hrefValue = anchor.Attributes["href"]?.Value;
                    if (!string.IsNullOrEmpty(hrefValue) && hrefValue.StartsWith("/questionnaires/"))
                    {
                        anchor.SetAttributeValue("href", hrefValue
                            .Replace("/questionnaires", $"/{languageCode}/get-to-an-answer-questionnaires"));
                    }
                }
            }
        }

        // if the external link is this site, change the language code 
        var externalLinkInput = doc.DocumentNode.SelectSingleNode("//input[@id='external-link-dest']");
        if (externalLinkInput != null && thisOrigin != null)
        {
            // if 'externalLinkInput.value' starts with 'thisOrigin' (https://*.support-for-care-leavers.education.gov.uk)
            // then replace the language code in the url with the current translation language code

            var valueAttribute = externalLinkInput.Attributes["value"];
            if (valueAttribute != null && !string.IsNullOrEmpty(valueAttribute.Value))
            {
                var url = new Uri(valueAttribute.Value);
                if (url.Host.Equals(thisOrigin))
                {
                    var pathParts = url.AbsolutePath.Split('/');

                    if (pathParts.Length > 1)
                    {
                        pathParts[1] = languageCode;
                    }
                    var newUrl = new UriBuilder(url) { Path = string.Join('/', pathParts) }.Uri;
                    externalLinkInput.SetAttributeValue("value", newUrl.ToString());
                }
            }
        }
    }
}
