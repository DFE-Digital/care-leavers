using System.Text;
using System.Xml.Linq;
using CareLeavers.Web.Contentful;
using Microsoft.AspNetCore.Mvc;

namespace CareLeavers.Web.Controllers;

public class SitemapController(IContentService contentService) : Controller
{
    [Route("/sitemap.xml")]
    [Route("/sitemap")]
    public async Task<IActionResult> Sitemap()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var hierarchy = await contentService.GetSiteHierarchy();
        
        // Get all slugs, but add the default locale of "en"
        if (hierarchy != null)
        {
            var slugs = hierarchy
                .Where(s => !s.ExcludeFromSitemap)
                .Select(s => Url.Action(
                    "GetContent",
                    "Contentful",
                    protocol: "https",
                    values: new { languageCode = "en", slug = s.Slug }))
                .ToList();
        
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        
            // add known .NET pages
            slugs.Add(Url.Action("CookiePolicy", "Pages", protocol: "https", values: new { languageCode = "en" }));
            slugs.Add(Url.Action("PrivacyPolicies", "Pages", protocol: "https", values: new { languageCode = "en" }));

            var xmlDoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                new XElement(ns + "urlset",
                    slugs.Select(slug => new XElement(ns + "url",
                        new XElement(ns + "loc", slug)
                    ))
                ));

            var sw = new Utf8StringWriter();
            xmlDoc.Save(sw);
        
            return Content(sw.ToString(), "application/xml");
        }

        return NotFound();
    }
}

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => Encoding.UTF8;
}