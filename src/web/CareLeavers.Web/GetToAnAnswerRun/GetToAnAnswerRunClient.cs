using System.Text;
using System.Web;
using Joonasw.AspNetCore.SecurityHeaders.Csp;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System.Text.RegularExpressions;

namespace CareLeavers.Web.GetToAnAnswerRun;

public class GetToAnAnswerRunClient(
    HttpClient httpClient, 
    IServiceProvider serviceProvider,
    ILogger<GetToAnAnswerRunClient> logger,
    IMemoryCache cache
) : IGetToAnAnswerRunClient {
    private readonly IConfiguration _configuration = serviceProvider.GetRequiredService<IConfiguration>();
    private readonly ICspNonceService _cspNonceService = serviceProvider.GetRequiredService<ICspNonceService>();
    private readonly IMemoryCache _cache = cache;

    public async Task<string> GetStartPageOrInitialState(string languageCode, string questionnaireSlug)
    {
        var cacheKey = $"gtaa:start:{languageCode}:{questionnaireSlug}";

        if (_cache.TryGetValue(cacheKey, out string? cachedHtml))
        {
            logger.LogDebug("Cache hit for GetStartPageOrInitialState: {slug}", questionnaireSlug);
            return cachedHtml;
        }

        var responseMessage = await httpClient.GetAsync(
            $"/questionnaires/{HttpUtility.UrlEncode(questionnaireSlug)}/start?embed=true");

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get start state for questionnaire {questionnaireSlug}");
        }
        
        var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
        var html = Encoding.UTF8.GetString(bytes);
        
        // Replace the base url with the local url so that the embedded content redirects to the correct page
        return SubstitutePageContent(languageCode, html);
    }

    public async Task<string> GetInitialState(string languageCode, string questionnaireSlug)
    {
        var cacheKey = $"gtaa:next:{languageCode}:{questionnaireSlug}";

        if (_cache.TryGetValue(cacheKey, out string? cachedHtml))
        {
            logger.LogDebug("Cache hit for GetInitialState: {slug}", questionnaireSlug);
            return cachedHtml;
        }

        var responseMessage = await httpClient.GetAsync(
            $"/questionnaires/{HttpUtility.UrlEncode(questionnaireSlug)}/next?embed=true");

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to get initial state for questionnaire {questionnaireSlug}");
        }

        var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
        var html = Encoding.UTF8.GetString(bytes);

        // Replace the base url with the local url so that the embedded content redirects to the correct page
        var substituted = SubstitutePageContent(languageCode, html);

        // Cache for 5 minutes - next pages are typically static per questionnaire
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        _cache.Set(cacheKey, substituted, cacheOptions);

        return substituted;
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
            throw new Exception($"Failed to get next state for questionnaire {questionnaireSlug}");
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
            throw new Exception($"Failed to get decorative image for questionnaire {questionnaireSlug}");
        }
    
        var stream = await response.Content.ReadAsStreamAsync();
        var contentType = response.Content.Headers.ContentType?.MediaType ?? "image/png";
    
        return (stream, contentType);
    }

    private string SubstitutePageContent(string languageCode, string html, string? thisOrigin = null)
    {
        var baseUrl = _configuration["GetToAnAnswer:BaseUrl"];
        var nonce = _cspNonceService.GetNonce();

        // Add nonce to script tags that don't have it
        html = Regex.Replace(html, @"<script\b(?!\s+nonce)([^>]*)>", match =>
        {
            var attrs = match.Groups[1].Value;
            if (string.IsNullOrWhiteSpace(attrs))
            {
                return $"<script nonce=\"{nonce}\">";
            }
            return $"<script{attrs} nonce=\"{nonce}\">";
        }, RegexOptions.IgnoreCase);

        // Add nonce to style tags that don't have it
        html = Regex.Replace(html, @"<style\b(?!\s+nonce)([^>]*)>", match =>
        {
            var attrs = match.Groups[1].Value;
            if (string.IsNullOrWhiteSpace(attrs))
            {
                return $"<style nonce=\"{nonce}\">";
            }
            return $"<style{attrs} nonce=\"{nonce}\">";
        }, RegexOptions.IgnoreCase);

        // Replace src="/..." or src=/... with baseUrl prefix
        html = Regex.Replace(html, @"src=[""']?(/[^""\s>]+)[""']?", match =>
        {
            var src = match.Groups[1].Value;
            return $"src=\"{baseUrl}{src}\"";
        }, RegexOptions.IgnoreCase);

        // Replace href="/..." or href=/... in link tags with baseUrl prefix (but not for /questionnaires/)
        html = Regex.Replace(html, @"<link\b([^>]*?)href=[""']?(/[^""\s>]+?)[""']?(\s|>)", match =>
        {
            var prefix = match.Groups[1].Value;
            var href = match.Groups[2].Value;
            var suffix = match.Groups[3].Value;

            // Only add baseUrl if it doesn't start with /questionnaires/
            if (!href.Contains("/questionnaires/"))
            {
                return $"<link{prefix}href=\"{baseUrl}{href}\"{suffix}";
            }
            return match.Value;
        }, RegexOptions.IgnoreCase);

        // Replace form action="/questionnaires/..." with local routes
        html = Regex.Replace(html, @"action=[""']?(/questionnaires[^""\s>]*)[""']?", match =>
        {
            var action = match.Groups[1].Value;
            var newAction = action.Replace("/questionnaires", $"/{languageCode}/get-to-an-answer-questionnaires");
            return $"action=\"{newAction}\"";
        }, RegexOptions.IgnoreCase);

        // Replace href="/questionnaires/..." in anchor tags
        html = Regex.Replace(html, @"<a\b([^>]*?)href=[""']?(/questionnaires[^""\s>]*)[""']?", match =>
        {
            var prefix = match.Groups[1].Value;
            var href = match.Groups[2].Value;
            var newHref = href.Replace("/questionnaires", $"/{languageCode}/get-to-an-answer-questionnaires");
            return $"<a{prefix}href=\"{newHref}\"";
        }, RegexOptions.IgnoreCase);

        // Replace external-link-dest input value if it matches thisOrigin
        if (!string.IsNullOrEmpty(thisOrigin))
        {
            html = Regex.Replace(html, 
                @"<input\b([^>]*?)id=[""']?external-link-dest[""']?([^>]*?)value=[""']?([^""\s>]+)[""']?",
                match =>
                {
                    var beforeId = match.Groups[1].Value;
                    var afterId = match.Groups[2].Value;
                    var value = match.Groups[3].Value;

                    try
                    {
                        var url = new Uri(value);
                        if (url.Host.Equals(thisOrigin, StringComparison.OrdinalIgnoreCase))
                        {
                            var pathParts = url.AbsolutePath.Split('/');
                            if (pathParts.Length > 1)
                            {
                                pathParts[1] = languageCode;
                            }
                            var newPath = string.Join('/', pathParts);
                            var newUrl = new UriBuilder(url) { Path = newPath }.Uri;
                            return $"<input{beforeId}id=\"external-link-dest\"{afterId}value=\"{newUrl}\"";
                        }
                    }
                    catch
                    {
                        // If URL parsing fails, leave it as is
                    }

                    return match.Value;
                },
                RegexOptions.IgnoreCase);
        }

        // Remove asp-add-nonce attributes
        html = Regex.Replace(html, @"\s*asp-add-nonce(?:=[""']?[^""'\s>]*[""']?)?", string.Empty, RegexOptions.IgnoreCase);

        // Self-close any input tags that aren't already self-closed
        html = Regex.Replace(html, @"<input\b([^>]+?)(?<!/)>", "<input$1 />", RegexOptions.IgnoreCase);

        return html;
    }
}