using System.Diagnostics.CodeAnalysis;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Contentful;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

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

    [Route("/{**slug}")]
    public async Task<IActionResult> GetContent(string slug)
    {
        var page = await contentService.GetPage(slug);

        if (page == null)
        {
            var config = await contentService.GetConfiguration();
            
            if (config!.Redirects.TryGetValue(slug, out var redirect))
            {
                Log.Logger.Information("Redirecting {Slug} to {Redirect}", slug, redirect);
                return RedirectToAction("GetContent", new { slug = redirect });
            }
            
            return NotFound();
        }

        return View("Page", page);
    }
}