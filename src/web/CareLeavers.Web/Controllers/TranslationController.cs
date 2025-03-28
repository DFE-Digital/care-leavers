using CareLeavers.Web.Configuration;
using CareLeavers.Web.Models.ViewModels;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

[Route("translate-this-website")]
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
    
    [Route("page/{page?}")]
    public async Task<IActionResult> Page(string? page)
    {
        var config = await contentfulConfiguration.GetConfiguration();

        if (!config.TranslationEnabled)
        {
            return NotFound();
        }

        var model = new TranslationViewModel()
        {
            Languages = await translationService.GetLanguages(),
            Page = page
        };
        
        return View("Index", model);
    }
}