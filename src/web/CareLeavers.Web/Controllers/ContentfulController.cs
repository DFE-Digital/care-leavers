using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Linq;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Contentful;
using Contentful.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace CareLeavers.Web.Controllers;

public class ContentfulController(IContentService contentService) : Controller
{
    public async Task<IActionResult> Homepage([FromServices] IContentfulConfiguration contentfulConfiguration)
    {
        var config = await contentfulConfiguration.GetConfiguration();
        return Redirect($"/{config.HomePage?.Slug}");
    }

    [ExcludeFromCodeCoverage(Justification = "Development only")]
    public async Task<IActionResult> ContentJson(string slug, [FromServices] IWebHostEnvironment environment)
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

    public async Task<IActionResult> Content(string slug)
    {
        var page = await contentService.GetPage(slug);

        if (page == null)
        {
            return NotFound();
        }

        return View("Page", page);
    }
}