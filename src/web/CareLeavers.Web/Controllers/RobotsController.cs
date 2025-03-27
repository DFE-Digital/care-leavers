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
        sb.AppendLine($"Disallow: {Url.Action("Index", "Translation", new { slug = "" })}");
        sb.AppendLine($"Disallow: {Url.Action("Index", "Translation", new { slug = "" })}/");
        sb.AppendLine($"Disallow: {Url.Action("PageNotFound", "Pages", new { languageCode = "en" })}");
        sb.AppendLine($"Disallow: {Url.Action("Error", "Pages", new { languageCode = "en" })}");
        sb.AppendLine($"Disallow: {Url.Action("ServiceUnavailable", "Pages", new { languageCode = "en" })}");

        foreach (var language in languages.Where(l => !l.Code.Equals("en", StringComparison.InvariantCultureIgnoreCase)))
        {
            sb.AppendLine($"Disallow: /{language.Code}/");
        }
        
        sb.AppendLine("Allow: /");
        sb.AppendLine();
        sb.AppendLine($"Sitemap: {Url.ActionLink(action: "Sitemap", controller: "Sitemap", protocol: "https")}");
        
        return Content(sb.ToString(), "text/plain");
    }
}