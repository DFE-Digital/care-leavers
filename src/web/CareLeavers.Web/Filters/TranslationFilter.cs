using CareLeavers.Web.CircuitBreaker.FairUsage;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Filters;

public sealed class TranslationFilter(
    IFusionCache fusionCache,
    ITranslationService translationService,
    FairUsageService fairUsageService,
    bool noCache = false) : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        string languageCode = context.RouteData.Values["languageCode"]?.ToString() ?? "en";

        if (await ShouldNotTranslate(languageCode))
        {
            await next();
            return;
        }

        if (fairUsageService.ShouldLimitUsage(FairUsageType.AzureTranslation))
        {
            context.Result = new RedirectToActionResult("TranslationUnavailable", "Pages", null);
            await next();
            return;
        }

        Stream originalBody = context.HttpContext.Response.Body;
        using MemoryStream memoryStream = new();
        context.HttpContext.Response.Body = memoryStream;

        await next();

        memoryStream.Seek(0, SeekOrigin.Begin);
        using StreamReader reader = new(memoryStream);

        string originalHtml = await reader.ReadToEndAsync();
        string? translatedHtml;

        (string? cacheKey, List<string> tags) = await ComputeCacheKeyAndTags(context.RouteData.Values, languageCode);

        if (!string.IsNullOrWhiteSpace(cacheKey))
            translatedHtml = await fusionCache.GetOrSetAsync<string?>(cacheKey,
                async _ => await translationService.TranslateHtml(originalHtml, languageCode), tags: tags);
        else translatedHtml = await translationService.TranslateHtml(originalHtml, languageCode);

        context.HttpContext.Response.ContentLength = null;
        context.HttpContext.Response.Body = originalBody;
        await context.HttpContext.Response.WriteAsync(translatedHtml ?? originalHtml);
    }

    private async Task<bool> ShouldNotTranslate(string languageCode)
        => languageCode.Equals("en") || (await translationService.GetLanguage(languageCode)).Code.Equals("en");

    private async Task<(string? CacheKey, List<string> Tags)> ComputeCacheKeyAndTags(RouteValueDictionary routeData,
        string languageCode)
    {
        if (noCache) return (null, []);

        string? slug = routeData["slug"]?.ToString();
        if (slug is not null) return ($"content:{slug}:language:{languageCode}", [slug]);

        string? identifier = routeData["identifier"]?.ToString();
        if (identifier is null) return (null, []);

        List<string> tags = [identifier];
        MaybeValue<PrintableCollection> collection =
            await fusionCache.TryGetAsync<PrintableCollection>($"collection:{identifier}");
        if (collection.HasValue) tags.AddRange(collection.Value.Content.Select(page => page.Slug).OfType<string>());
        return ($"collection:{identifier}:language:{languageCode}", tags);
    }
}