using System.Text;
using System.Xml.Linq;
using CareLeavers.Web.Contentful;
using Microsoft.AspNetCore.Mvc;

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
        
        // add known .NET pages
        slugs.Add("cookiepolicy", Url.Action("CookiePolicy", "Pages", new { languageCode = "en" })!.TrimStart('/'));
        slugs.Add("privacypolicies", Url.Action("PrivacyPolicies", "Pages", new { languageCode = "en" })!.TrimStart('/'));

        var xmlDoc = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement(ns + "urlset",
                slugs.Select(slug => new XElement(ns + "url",
                    new XElement(ns + "loc", $"{configuration["BaseUrl"]}/{slug.Value}")
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