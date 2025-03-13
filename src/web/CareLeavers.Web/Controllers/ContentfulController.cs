using System.Diagnostics.CodeAnalysis;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Contentful;
using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

    [Route("/json/configuration")]
    [ExcludeFromCodeCoverage(Justification = "Development only")]
    public async Task<IActionResult> GetConfigurationAsJson([FromServices] IWebHostEnvironment environment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!environment.IsDevelopment())
        {
            return NotFound();
        }

        var configuration = await contentService.GetConfiguration();

        return Content(JsonConvert.SerializeObject(configuration, Constants.SerializerSettings), "application/json");
    }
    
    [Route("/json/{contentType}/{id}")]
    [ExcludeFromCodeCoverage(Justification = "Development only")]
    public async Task<IActionResult> GetContentAsJson(string contentType, string id, [FromServices] IWebHostEnvironment environment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        if (!environment.IsDevelopment())
        {
            return NotFound();
        }

        var returnObject = new object();
        
        if (contentType == Grid.ContentType)
        {
            returnObject = await contentService.Hydrate(new Grid() { Sys = new SystemProperties() { Id = id } });
        } 
        else if (contentType == RichContentBlock.ContentType)
        {
            returnObject = await contentService.Hydrate(new RichContentBlock() { Sys = new SystemProperties() { Id = id } });
        }
        else if (contentType == Banner.ContentType)
        {
            returnObject = await contentService.Hydrate(new Banner() { Sys = new SystemProperties() { Id = id } });
        }

        return Content(JsonConvert.SerializeObject(returnObject, Constants.SerializerSettings), "application/json");
    }

    [Route("/{**slug}")]
    public async Task<IActionResult> GetContent(string slug)
    {
        var page = await contentService.GetPage(slug);

        if (page == null)
        {
            return NotFound();
        }

        return View("Page", page);
    }
}