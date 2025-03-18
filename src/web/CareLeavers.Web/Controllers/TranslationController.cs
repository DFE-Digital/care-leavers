using CareLeavers.Web.Configuration;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

[Route("translation")]
public class TranslationController(
    ITranslationService translationService,
    IContentfulConfiguration contentfulConfiguration) : Controller
{
    public async Task<IActionResult> Index()
    {
        var config = await contentfulConfiguration.GetConfiguration();

        if (!config.TranslationEnabled)
        {
            return NotFound();
        }
        
        var translations = await translationService.GetLanguages();
        
        return View(translations);
    }
}