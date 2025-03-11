using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Linq;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Contentful;
using CareLeavers.Web.Filters;
using Contentful.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace CareLeavers.Web.Controllers;

[Route("/")]
public class ContentfulController(IContentService contentService) : Controller
{
    [Route("/")]
    public async Task<IActionResult> Homepage(
        [FromServices] IContentfulConfiguration contentfulConfiguration,
        [FromQuery] string? languageCode = null)
    {
        var config = await contentfulConfiguration.GetConfiguration();
        return RedirectToAction("GetContent", new { slug = config.HomePage?.Slug, languageCode });
    }

    [Route("/json/{**slug}")]
    [ExcludeFromCodeCoverage(Justification = "Development only")]
    public async Task<IActionResult> GetContentAsJson(string slug, [FromServices] IWebHostEnvironment environment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!environment.IsDevelopment())
        {
            return NotFound();
        }

        var page = await contentService.GetPage(slug);

        return Content(JsonConvert.SerializeObject(page, Constants.SerializerSettings), "application/json");
    }

    [Route("/{slug}")]
    [Route("/{languageCode}/{slug}")]
    [ContentfulCaching]
    public async Task<IActionResult> GetContent(string slug, string? languageCode)
    {
        if (languageCode == "en")
        {
            return RedirectToAction("GetContent", new { slug, languageCode = string.Empty });
        }
        
        var page = await contentService.GetPage(slug);

        if (page == null)
        {
            return NotFound();
        }

        return View("Page", page);
    }
}