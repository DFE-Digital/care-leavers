using CareLeavers.Web.Configuration;
using CareLeavers.Web.Models.ViewModels;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

[Route("translation")]
public class TranslationController(
    ITranslationService translationService,
    IContentfulConfiguration contentfulConfiguration) : Controller
{
    [Route("{slug?}")]
    public async Task<IActionResult> Index(string? slug)
    {
        var config = await contentfulConfiguration.GetConfiguration();

        if (!config.TranslationEnabled)
        {
            return NotFound();
        }

        var model = new TranslationViewModel()
        {
            Languages = await translationService.GetLanguages(),
            Slug = slug
        };
        
        return View(model);
    }
}