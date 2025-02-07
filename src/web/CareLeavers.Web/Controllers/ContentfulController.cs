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

[Route("/")]
public class ContentfulController(IContentService contentService) : Controller
{
    [Route("/")]
    public async Task<IActionResult> Homepage([FromServices] IContentfulConfiguration contentfulConfiguration)
    {
        var config = await contentfulConfiguration.GetConfiguration();
        return Redirect($"/{config.HomePage?.Slug}");
    }

    [Route("/json/{**slug}")]
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

    [Route("/{**slug}")]
    public async Task<IActionResult> Contentful(string slug)
    {
        var page = await contentService.GetPage(slug);

        if (page == null)
        {
            return NotFound();
        }

        return View("Page", page);
    }

    [Route("sitemap.xml")]
    public async Task<IActionResult> Sitemap([FromServices] IConfiguration configuration)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var slugs = await contentService.GetSiteSlugs();
        
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        var xmlDoc = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement(ns + "urlset",
                slugs.Select(slug => new XElement(ns + "url",
                    new XElement(ns + "loc", $"{configuration["BaseUrl"]}/{slug}")
                ))
            ));

        var sw = new Utf8StringWriter();
        xmlDoc.Save(sw);
        
        return Content(sw.ToString(), "application/xml");
    }
}

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => Encoding.UTF8;
}