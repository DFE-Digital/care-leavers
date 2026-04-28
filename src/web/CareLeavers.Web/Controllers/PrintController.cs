using CareLeavers.Web.Contentful;
using CareLeavers.Web.Filters;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Pdf;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

public class PrintController(
    IContentService contentService,
    ITranslationService translationService,
    IConfiguration configuration,
    ILogger<PrintController> logger) : Controller
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
    public async Task<IActionResult> DownloadPdf([FromServices] IPdfGenerator pdfGenerator, string identifier,
        string languageCode)
    {
        string? url = Url.ActionLink("GetPrintableCollection", "Print", new { identifier, languageCode },
            host: configuration["Pdf:CallbackHost"] ?? Request.Host.Value);
        if (string.IsNullOrWhiteSpace(url)) return BadRequest();

        PrintableCollection? collection = await contentService.GetPrintableCollection(identifier);
        if (collection is null) return NotFound();

        List<string> tags = [$"pc-{identifier}"];
        tags.AddRange(collection.Content.Select(p => p.Slug).Where(slug => !string.IsNullOrWhiteSpace(slug))
            .OfType<string>());

        string fileName = $"{identifier}{(languageCode.Equals("en") ? "" : $"-{languageCode}")}.pdf";
        byte[] pdfBytes;

        try
        {
            pdfBytes = await pdfGenerator.Generate(url, identifier, languageCode, tags);
        }
        catch (HttpRequestException e)
        {
            logger.LogError(e, "Error Generating PDF - Redirecting to Print Page Instead..");
            return RedirectToAction("GetPrintableCollection", new { identifier, languageCode });
        }

        if (pdfBytes.Length == 0) return NotFound();

        Response.Headers.Append("Content-Disposition", $"inline;{fileName}");
        return File(pdfBytes, contentType: "application/pdf");
    }
}