using System.Net.Http.Headers;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Contentful;
using CareLeavers.Web.Filters;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Controllers;

public class PrintController(IContentService contentService, ITranslationService translationService, IOptions<PdfGenerationOptions> pdfOptions, IFusionCache fusionCache) : Controller
{
    [Route("/print/{identifier}")]
    [Route("/print/{languageCode}/{identifier}")]
    [Translation]
    public async Task<IActionResult> GetPrintableCollection(string identifier, string languageCode)
    {
        if (string.IsNullOrEmpty(languageCode))
        {
            return RedirectToAction("GetPrintableCollection", new { identifier, languageCode = "en" });
        }

        var config = await contentService.GetConfiguration();

        if (config == null)
        {
            return NotFound();
        }
        

        var languages = new List<string>();
        if (config is { TranslationEnabled: true })
        {
            languages.AddRange((await translationService.GetLanguages()).Select(l => l.Code));
        }
        if (languages.Count == 0)
        {
            languages.Add("en");
        }
        
        if (!languages.Contains(languageCode))
        {
            return RedirectToAction("GetPrintableCollection", new { identifier, languageCode = "en" });
        }
        
        var collection = await contentService.GetPrintableCollection(identifier);

        if (collection == null)
        {
            return NotFound();
        }
        
        return View("Collection", collection);
    }

    [Route("/pdf/{languageCode}/{identifier}")]
    public async Task<IActionResult> DownloadPdf(string identifier, string languageCode)
    {


        var collection = await contentService.GetPrintableCollection(identifier);
        if (collection == null)
            return NotFound();

        // Add tags for each page in the collection, so we expire it if a page changes
        var tags = collection.Content.Select(p => p.Slug!).ToList();
        
        // Also add our printable collection identifier as a tag too
        tags.Add($"pc-{identifier}");

        try
        {
            var pdf = await fusionCache.GetOrSetAsync<byte[]>($"pdf:{identifier}:{languageCode}", async token =>
            {
                var config = await contentService.GetConfiguration();

                var url = Url.ActionLink("GetPrintableCollection", "Print", new { identifier, languageCode });

                if (config == null)
                    return [];

                var client = new HttpClient();
                var request = new HttpRequestMessage();

                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri("https://api.pdfendpoint.com/v1/convert");
                request.Headers.Authorization =
                    AuthenticationHeaderValue.Parse($"Bearer {pdfOptions.Value.ApiKey}");
                request.Content =
                    new StringContent($$"""
                                        {
                                            "url": "{{url}}",
                                            "sandbox": "{{(pdfOptions.Value.Sandbox ? "true" : "false")}}", 
                                            "delivery_mode": "inline", 
                                            "title": "{{collection.Title}}", 
                                            "author": "{{config.ServiceName}}",
                                            "print_media": "true",
                                            "user_agent": "PDF Renderer - twitterbot"
                                        }
                                        """);

                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                using var response = await client.SendAsync(request, token);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsByteArrayAsync(token);
            }, tags: tags);

            if (pdf.Length > 0)
            {
                Response.Headers.Append("Content-Disposition",$"inline;{identifier}.pdf");
                return File(pdf, contentType: "application/pdf");
            }
        }
        catch (HttpRequestException)
        {
            // If we can't generate our printable PDF, redirect to the print page instead
            return RedirectToAction("GetPrintableCollection", new { identifier, languageCode });
        }

        return NotFound();

    }
}