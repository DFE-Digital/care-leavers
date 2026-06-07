using CareLeavers.Web.Contentful;
using CareLeavers.Web.Filters;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

public class PrintController(IContentService contentService, ITranslationService translationService) : Controller
{
    [Route("/print/{identifier}")]
    [Route("/print/{languageCode}/{identifier}")]
    [Translation]
    public async Task<IActionResult> GetPrintableCollection(string identifier, string languageCode)
    {
        if (string.IsNullOrWhiteSpace(languageCode)) 
            return RedirectToAction("GetPrintableCollection", new { identifier, languageCode = "en" });
        
        ContentfulConfigurationEntity? config = await contentService.GetConfiguration();
        
        switch (config)
        {
            case { TranslationEnabled: true }:
            {
                TranslationLanguage language = await translationService.GetLanguage(languageCode);
                if (!languageCode.Equals(language.Code, StringComparison.InvariantCultureIgnoreCase)) 
                    return RedirectToAction("GetPrintableCollection", new { identifier, languageCode = "en" });
                break;
            }
            case null:
                return NotFound();
        }

        PrintableCollection? collection = await contentService.GetPrintableCollection(identifier);
        return collection is not null ? View("Collection", collection) : NotFound();
    }
}