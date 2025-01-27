using System.Text;
using System.Xml.Linq;
using CareLeavers.Web.Caching;
using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Configuration;
using Contentful.Core.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace CareLeavers.Web.Controllers;

public class ContentfulController(
    IContentfulClient contentfulClient,
    IWebHostEnvironment environment,
    IDistributedCache distributedCache) : Controller
{
    private static readonly JsonSerializerSettings ContentfulSerializerSettings = new()
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = false
            }
        },
        Formatting = Formatting.Indented,
        Converters = new List<JsonConverter>
        {
            new ExtensionJsonConverter(),
        }
    };
    
    public const string HomepageSlug = "home";
    
    [Route("/")]
    public IActionResult Homepage()
    {
        return Redirect($"/{HomepageSlug}");
    }
    
    [Route("/{**slug}")]
    public async Task<IActionResult> Content(string slug, [FromQuery] bool isJson = false)
    {
        var page = await distributedCache.GetOrSetAsync($"content:{slug}", async () =>
        {
            var pages = new QueryBuilder<Page>()
                .ContentTypeIs("page")
                .FieldEquals(c => c.Slug, slug)
                .Limit(1);

            var pageEntries = await contentfulClient.GetEntries(pages);

            return pageEntries.FirstOrDefault();
        });
        
        if (environment.IsDevelopment() && isJson)
        {
            return Content(JsonConvert.SerializeObject(page, ContentfulSerializerSettings), "application/json");
        }
        
        return View("Page", page);
    }

    [Route("sitemap.xml")]
    public async Task<IActionResult> Sitemap()
    {
        var page = await distributedCache.GetOrSetAsync($"content:sitemap", async () =>
        {
            var pages = new QueryBuilder<Page>()
                .ContentTypeIs("page")
                .SelectFields(x => new {x.Slug});

            var pageEntries = await contentfulClient.GetEntries(pages);

            XNamespace ns    = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XNamespace xsiNs = "http://www.w3.org/2001/XMLSchema-instance";

            var xmlDoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                new XElement(ns + "urlset",
                    pageEntries.Select(x => new XElement(ns + "url",
                        new XElement(ns + "loc", $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{x.Slug}")
                    ))
                ));

            var sw = new Utf8StringWriter();
            xmlDoc.Save(sw);
            
            return sw.ToString();
        });
        
        /*
         new XAttribute(XNamespace.Xmlns + "xsi", xsiNs),
           new XAttribute(xsiNs + "schemaLocation",
               "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd"),
         */

        return Content(page ?? "<urlset/>", "application/xml");
    }
}

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => Encoding.UTF8;
}