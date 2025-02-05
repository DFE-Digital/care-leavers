using System.Diagnostics.CodeAnalysis;
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

[Route("/")]
public class ContentfulController(
    IDistributedCache distributedCache, 
    IContentfulClient contentfulClient) : Controller
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

        var page = await GetContentfulPage(slug);
        
        return Content(JsonConvert.SerializeObject(page, ContentfulSerializerSettings), "application/json");
    }
    
    [Route("/{**slug}")]
    public async Task<IActionResult> Contentful(string slug)
    {
        var page = await GetContentfulPage(slug);

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
        
        var page = await distributedCache.GetOrSetAsync($"content:sitemap", async () =>
        {
            var pages = new QueryBuilder<Page>()
                .ContentTypeIs("page")
                .SelectFields(x => new {x.Slug});

            var pageEntries = await contentfulClient.GetEntries(pages);
            
            XNamespace ns    = "http://www.sitemaps.org/schemas/sitemap/0.9";

            var xmlDoc = new XDocument(
                new XDeclaration("1.0", "UTF-8", null),
                new XElement(ns + "urlset",
                    pageEntries.Select(x => new XElement(ns + "url",
                        new XElement(ns + "loc", $"{configuration["BaseUrl"]}/{x.Slug}")
                    ))
                ));

            var sw = new Utf8StringWriter();
            xmlDoc.Save(sw);
            
            return sw.ToString();
        });

        return Content(page ?? "<urlset/>", "application/xml");
    }

    private Task<Page?> GetContentfulPage(string slug)
    {
        return distributedCache.GetOrSetAsync($"content:{slug}", async () =>
        {
            var pages = new QueryBuilder<Page>()
                .ContentTypeIs(Page.ContentType)
                .FieldEquals(c => c.Slug, slug)
                .Limit(1);

            var pageEntries = await contentfulClient.GetEntries(pages);

            return pageEntries.FirstOrDefault();
        });
    }
}

public class Utf8StringWriter : StringWriter
{
    public override Encoding Encoding => Encoding.UTF8;
}