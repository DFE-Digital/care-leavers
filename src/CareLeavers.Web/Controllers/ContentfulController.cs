using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Configuration;
using Contentful.Core.Search;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CareLeavers.Web.Controllers;

public class ContentfulController(IContentfulClient contentfulClient, IWebHostEnvironment environment) : Controller
{
    private IContentfulClient _contentfulClient = contentfulClient;
    
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
        var pages = new QueryBuilder<Page>()
            .ContentTypeIs("page")
            .FieldEquals(c => c.Slug, slug)
            .Limit(1);

        var pageEntries = await _contentfulClient.GetEntries(pages);
        
        var page = pageEntries.FirstOrDefault();
        
        if (environment.IsDevelopment() && isJson)
        {
            return Content(JsonConvert.SerializeObject(page, ContentfulSerializerSettings), "application/json");
        }
        
        return View("Page", page);
    }
}