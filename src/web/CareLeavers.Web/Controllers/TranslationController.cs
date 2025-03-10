using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

[Route("translation")]
public class TranslationController(ITranslationService translationService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var translations = await translationService.GetLanguages();
        
        return View(translations);
    }
}