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
        languageCode ??= "en";
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
    [Translation]
    public async Task<IActionResult> GetContent(string slug, string? languageCode)
    {
        if (string.IsNullOrEmpty(languageCode))
        {
            return RedirectToAction("GetContent", new { slug, languageCode = "en" });
        }
        
        var page = await contentService.GetPage(slug);

        if (page == null)
        {
            return NotFound();
        }

        return View("Page", page);
    }
}