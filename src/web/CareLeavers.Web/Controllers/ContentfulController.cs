using System.Diagnostics.CodeAnalysis;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Contentful;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Filters;
using CareLeavers.Web.Translation;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CareLeavers.Web.Controllers;

public class PrintController(IContentService contentService, ITranslationService translationService) : Controller
{
    [Route("/print/{slug}")]
    [Route("/{languageCode}/print/{slug}")]
    [Translation]
    public async Task<IActionResult> GetPrintableBooklet(string slug, string languageCode)
    {
        if (string.IsNullOrEmpty(languageCode))
        {
            return RedirectToAction("GetPrintableBooklet", new { slug, languageCode = "en" });
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
            return RedirectToAction("GetPrintableBooklet", new { slug, languageCode = "en" });
        }
        
        var booklet = await contentService.GetPrintableBooklet(slug);

        if (booklet == null)
        {
            return NotFound();
        }

        return View("Booklet", booklet);
    }
}