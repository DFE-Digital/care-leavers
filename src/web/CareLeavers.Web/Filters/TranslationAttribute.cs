using System.Text;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Models.Content;
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

    public bool NoCache { get; init; } = false;
    
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
        var identifier = context.RouteData.Values["identifier"]?.ToString();
        var languages = new List<string>();
        if (config.TranslationEnabled)
        {
            languages.AddRange((await translationService.GetLanguages()).Select(l => l.Code));
        }
        if (languages.Count == 0)
        {
            languages.Add("en");
        }
        
        if ((slug == null && identifier == null) || 
            !config.TranslationEnabled ||
            string.IsNullOrEmpty(languageCode) || 
            languageCode == "en" ||
            !languages.Contains(languageCode)
            )
        {
            await base.OnActionExecutionAsync(context, next);
            return;
        }

        MaybeValue<byte[]?> cachedResponse = MaybeValue<byte[]?>.None;

        if (!NoCache)
        {
            if (slug != null)
            {
                cachedResponse = await fusionCache.TryGetAsync<byte[]?>($"content:{slug}:language:{languageCode}");
            }
            else if (identifier != null)
            {
                cachedResponse =
                    await fusionCache.TryGetAsync<byte[]?>($"collection:{identifier}:language:{languageCode}");
            }
        }

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
        var identifier = context.RouteData.Values["identifier"]?.ToString();
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

        if ((slug == null && identifier == null) || 
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

        if (!NoCache)
        {
            if (slug != null)
            {
                await fusionCache.SetAsync($"content:{slug}:language:{languageCode}", _memoryStream.ToArray(),
                    tags: [slug]);
            }
            else if (identifier != null)
            {
                var collection = await fusionCache.TryGetAsync<PrintableCollection>($"collection:{identifier}");
                List<string>? tags = [];
                if (collection is { HasValue: true, Value: not null })
                {
                    tags = collection.Value.Content.Select(p => p.Slug!).ToList();
                }

                tags.Add(identifier);

                await fusionCache.SetAsync($"collection:{identifier}:language:{languageCode}", _memoryStream.ToArray(),
                    tags: tags);
            }
        }
    }
}