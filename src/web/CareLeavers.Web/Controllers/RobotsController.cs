using System.Text;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

public class RobotsController (IContentfulConfiguration contentfulConfiguration, ITranslationService translationService) : Controller
{
    [Route("/robots.txt")]
    public async Task<IActionResult> Robots()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var config = await contentfulConfiguration.GetConfiguration();
        var languages = new List<TranslationLanguage>();
        if (config.TranslationEnabled)
        {
            languages.AddRange(await translationService.GetLanguages());
        }
        
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("User-agent: *");
        sb.AppendLine("Disallow: /translation");
        sb.AppendLine("Disallow: /en/404");

        foreach (var language in languages.Where(l => !l.Code.Equals("en", StringComparison.InvariantCultureIgnoreCase)))
        {
            sb.AppendLine($"Disallow: /{language.Code}/");
        }
        
        sb.AppendLine("Allow: /");
        sb.AppendLine();
        sb.AppendLine($"Sitemap: {Url.ActionLink("Sitemap", "Sitemap", protocol: "https")}");
        
        return Content(sb.ToString(), "text/plain");
    }
}