using System.Text;
using System.Web;
using Joonasw.AspNetCore.SecurityHeaders.Csp;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System.Text.RegularExpressions;

namespace CareLeavers.Web.GetToAnAnswerRun;

public partial class GetToAnAnswerRunClient(
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
            throw new Exception($"Failed to get questionnaire page for {questionnaireSlug}");
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
        var substitutionContext = CreateSubstitutionContext(languageCode, thisOrigin);

        html = ApplyNonceSubstitutions(html, substitutionContext);
        html = ApplyUrlRewriting(html, substitutionContext);
        html = ApplyAttributeCleanup(html, substitutionContext);

        return html;
    }

    private SubstitutionContext CreateSubstitutionContext(string languageCode, string? thisOrigin) =>
        new(
            BaseUrl: _configuration["GetToAnAnswer:BaseUrl"],
            Nonce: _cspNonceService.GetNonce(),
            LanguageCode: languageCode,
            ThisOrigin: thisOrigin,
            RegexTimeout: TimeSpan.FromMilliseconds(500)
        );

    private static string ApplyNonceSubstitutions(string html, SubstitutionContext context)
    {
        html = AddNonceToTag(html, "script", context.Nonce);
        html = AddNonceToTag(html, "style", context.Nonce);
        return html;
    }

    private string ApplyUrlRewriting(string html, SubstitutionContext context)
    {
        html = SrcRegex().Replace(html, match =>
        {
            var src = match.Groups[1].Value;
            return $"src=\"{context.BaseUrl}{src}\"";
        });

        html = LinkHrefRegex().Replace(html, match =>
            ReplaceLinkHref(match, context));

        html = ReplaceQuestionnairePath(html, "action", context.LanguageCode, context.RegexTimeout);
        html = ReplaceQuestionnairePath(html, "href", context.LanguageCode, context.RegexTimeout, isAnchor: true);

        if (!string.IsNullOrEmpty(context.ThisOrigin))
        {
            html = ReplaceExternalLinkDestination(html, context.ThisOrigin, context.LanguageCode, context.RegexTimeout);
        }

        return html;
    }

    private static string ApplyAttributeCleanup(string html, SubstitutionContext context)
    {
        html = AspAddNonceRegex().Replace(html, string.Empty);
        html = InputTagRegex().Replace(html, "<input$1 />");
        return html;
    }

    private string ReplaceLinkHref(Match match, SubstitutionContext context)
    {
        var prefix = match.Groups[1].Value;
        var href = match.Groups[2].Value;
        var suffix = match.Groups[3].Value;

        if (!href.Contains("/questionnaires/"))
        {
            return $"<link{prefix}href=\"{context.BaseUrl}{href}\"{suffix}";
        }
        return match.Value;
    }

    private record SubstitutionContext(
        string BaseUrl,
        string Nonce,
        string LanguageCode,
        string? ThisOrigin,
        TimeSpan RegexTimeout
    );

    private static string AddNonceToTag(string html, string tagName, string nonce)
    {
        var pattern = new Regex($@"<{tagName}\b(?!\s+nonce)([^>]*)>", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500));
        return pattern.Replace(html, match =>
        {
            var attrs = match.Groups[1].Value;
            if (string.IsNullOrWhiteSpace(attrs))
            {
                return $"<{tagName} nonce=\"{nonce}\">";
            }
            return $"<{tagName}{attrs} nonce=\"{nonce}\">";
        });
    }

    private static string ReplaceQuestionnairePath(string html, string attribute, string languageCode, TimeSpan timeout, bool isAnchor = false)
    {
        if (isAnchor)
        {
            return AnchorHrefRegex().Replace(html, match =>
            {
                var prefix = match.Groups[1].Value;
                var href = match.Groups[2].Value;
                var newHref = href.Replace("/questionnaires", $"/{languageCode}/get-to-an-answer-questionnaires");
                return $"<a{prefix}href=\"{newHref}\"";
            });
        }
        else
        {
            var pattern = new Regex($@"{attribute}=[""']?(/questionnaires[^""\s>]*)[""']?", RegexOptions.IgnoreCase, timeout);
            return pattern.Replace(html, match =>
            {
                var path = match.Groups[1].Value;
                var newPath = path.Replace("/questionnaires", $"/{languageCode}/get-to-an-answer-questionnaires");
                return $"{attribute}=\"{newPath}\"";
            });
        }
    }

    private static string ReplaceExternalLinkDestination(string html, string thisOrigin, string languageCode, TimeSpan timeout)
    {
        return ExternalLinkDestRegex().Replace(html,
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
            });
    }

    [GeneratedRegex(@"src=[""']?(/[^""\s>]+)[""']?", RegexOptions.IgnoreCase)]
    private static partial Regex SrcRegex();

    [GeneratedRegex(@"<link\b([^>]*?)href=[""']?(/[^""\s>]+?)[""']?(\s|>)", RegexOptions.IgnoreCase)]
    private static partial Regex LinkHrefRegex();

    [GeneratedRegex(@"<a\b([^>]*?)href=[""']?(/questionnaires[^""\s>]*)[""']?", RegexOptions.IgnoreCase)]
    private static partial Regex AnchorHrefRegex();

    [GeneratedRegex(@"\s*asp-add-nonce(?:=[""']?[^""'\s>]*[""']?)?", RegexOptions.IgnoreCase)]
    private static partial Regex AspAddNonceRegex();

    [GeneratedRegex(@"<input\b([^>]+?)(?<!/)>", RegexOptions.IgnoreCase)]
    private static partial Regex InputTagRegex();

    [GeneratedRegex(@"<input\b([^>]*?)id=[""']?external-link-dest[""']?([^>]*?)value=[""']?([^""\s>]+)[""']?", RegexOptions.IgnoreCase)]
    private static partial Regex ExternalLinkDestRegex();
}