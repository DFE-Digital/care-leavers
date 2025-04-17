using System.Text;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Filters;

public class TranslationAttribute : ActionFilterAttribute
{
    private MemoryStream? _memoryStream;
    private Stream? _originalBodyStream;

    public string? HardcodedSlug { get; init; }
    
    public override async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        _memoryStream = null;
        _originalBodyStream = null;

        var fusionCache = context.HttpContext.RequestServices.GetRequiredService<IFusionCache>();
        var translationService = context.HttpContext.RequestServices.GetRequiredService<ITranslationService>();
        var contentfulConfiguration = context.HttpContext.RequestServices.GetRequiredService<IContentfulConfiguration>();
        var config = await contentfulConfiguration.GetConfiguration();

        var slug = HardcodedSlug ?? context.RouteData.Values["slug"]?.ToString();
        var languageCode = context.RouteData.Values["languageCode"]?.ToString();
        var languages = new List<string>();
        if (config.TranslationEnabled)
        {
            languages.AddRange((await translationService.GetLanguages()).Select(l => l.Code));
        }
        if (languages.Count == 0)
        {
            languages.Add("en");
        }
        
        if (slug == null || 
            !config.TranslationEnabled ||
            string.IsNullOrEmpty(languageCode) || 
            languageCode == "en" ||
            !languages.Contains(languageCode)
            )
        {
            await base.OnActionExecutionAsync(context, next);
            return;
        }

        var cachedResponse = await fusionCache.TryGetAsync<byte[]?>($"content:{slug}:language:{languageCode}");

        if (cachedResponse is { HasValue: true, Value: not null })
        {
            context.Result = new ContentResult
            {
                Content = Encoding.UTF8.GetString(cachedResponse.Value),
                ContentType = "text/html"
            };

            return;
        }

        _memoryStream = new MemoryStream();
        _originalBodyStream = context.HttpContext.Response.Body;
        context.HttpContext.Response.Body = _memoryStream;

        await base.OnActionExecutionAsync(context, next);
    }

    public override async Task OnResultExecutionAsync(
        ResultExecutingContext context,
        ResultExecutionDelegate next)
    {
        await base.OnResultExecutionAsync(context, next);
        
        var translationService = context.HttpContext.RequestServices.GetRequiredService<ITranslationService>();
        var contentfulConfiguration = context.HttpContext.RequestServices.GetRequiredService<IContentfulConfiguration>();
        var config = await contentfulConfiguration.GetConfiguration();
        var fusionCache = context.HttpContext.RequestServices.GetRequiredService<IFusionCache>();

        var slug = HardcodedSlug ?? context.RouteData.Values["slug"]?.ToString();
        var languageCode = context.RouteData.Values["languageCode"]?.ToString();
        var languages = new List<string>();
        if (config.TranslationEnabled)
        {
            languages.AddRange((await translationService.GetLanguages()).Select(l => l.Code));
        }
        else
        {
            languages.Add("en");
        }

        if (slug == null || 
            string.IsNullOrEmpty(languageCode) || 
            _memoryStream == null || 
            _originalBodyStream == null || 
            !languages.Contains(languageCode))
        {
            return;
        }


        _memoryStream.Seek(0, SeekOrigin.Begin);

        var responseBody = await new StreamReader(_memoryStream).ReadToEndAsync();
        
        var translatedHtml = await translationService.TranslateHtml(responseBody, languageCode);
        if (string.IsNullOrEmpty(translatedHtml))
        {
            _memoryStream.Seek(0, SeekOrigin.Begin);
        }
        else
        {
            await _memoryStream.DisposeAsync();
            _memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(translatedHtml));
        }

        context.HttpContext.Response.Body = _originalBodyStream!;
        await context.HttpContext.Response.Body!.WriteAsync(_memoryStream.ToArray());
        
        _memoryStream.Seek(0, SeekOrigin.Begin);
        await fusionCache.SetAsync($"content:{slug}:language:{languageCode}", _memoryStream.ToArray(), tags: [slug]);
    }
}