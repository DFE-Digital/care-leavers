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

public class SitemapController(IContentService contentService) : Controller
{
    [Route("/sitemap")]
    [Route("/sitemap.xml")]
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