using CareLeavers.Web.Models.Content;
using Contentful.Core;
using Contentful.Core.Search;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CareLeavers.Web.Controllers;

public class ContentfulController(IContentfulClient contentfulClient) : Controller
{
    private IContentfulClient _contentfulClient = contentfulClient;
    
    public const string HomepageSlug = "home";
    
    [Route("/")]
    public IActionResult Homepage()
    {
        return Redirect($"/{HomepageSlug}");
    }
    
    [Route("/{**slug}")]
    public async Task<IActionResult> Content(string slug)
    {
        var pages = new QueryBuilder<Page>()
            .ContentTypeIs("page")
            .FieldEquals(c => c.Slug, slug)
            .Limit(1);

        var pageEntries = await _contentfulClient.GetEntries(pages);
        
        var page = pageEntries.FirstOrDefault();

        var json = JsonConvert.SerializeObject(pageEntries);
        
        return View("Page", page);
    }
}