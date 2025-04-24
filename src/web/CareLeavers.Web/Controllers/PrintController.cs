using System.Diagnostics.CodeAnalysis;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Contentful;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Filters;
using CareLeavers.Web.Translation;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CareLeavers.Web.Controllers;

[Route("/")]
public class ContentfulController(IContentService contentService, ITranslationService translationService) : Controller
{
    [Route("/")]
    public async Task<IActionResult> Homepage(
        [FromServices] IContentfulConfiguration contentfulConfiguration,
        [FromQuery] string? languageCode = null)
    {
        languageCode ??= "en";
        var config = await contentfulConfiguration.GetConfiguration();
        return RedirectToAction("GetContent", new { slug = config.HomePage?.Slug, languageCode });
    }
    
    [Route("/en")]
    [Route("/en/en")]
    public async Task<IActionResult> NoSlug(
        [FromServices] IContentfulConfiguration contentfulConfiguration)
    {
        var config = await contentfulConfiguration.GetConfiguration();
        return RedirectToAction("GetContent", new { slug = config.HomePage?.Slug, languageCode = "en" });
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
            returnObject = new Grid() { Sys = new SystemProperties() { Id = id } };
        } 
        else if (contentType == RichContentBlock.ContentType)
        {
            returnObject = new RichContentBlock() { Sys = new SystemProperties() { Id = id } };
        }
        else if (contentType == Banner.ContentType)
        {
            returnObject = new Banner() { Sys = new SystemProperties() { Id = id } };
        }
        else if (contentType == StatusChecker.ContentType)
        {
            returnObject = new StatusChecker() { Sys = new SystemProperties() { Id = id } };
        }

        returnObject = await contentService.Hydrate(returnObject);

        return Content(JsonConvert.SerializeObject(returnObject, Constants.SerializerSettings), "application/json");
    }
    
    [Route("/print/{slug}")]
    [Route("/{languageCode}/print/{slug}")]
    [Translation]
    public async Task<IActionResult> GetPrintableBooklet(string slug, string languageCode)
    {
        if (string.IsNullOrEmpty(languageCode))
        {
            return RedirectToAction("GetPrintableBooklet", new { slug, languageCode = "en" });
        }
    }
    
    [Route("/{slug}")]
    [Route("/{languageCode}/{slug}")]
    [Translation]
    public async Task<IActionResult> GetContent(string slug, string? languageCode)
    {
        if (string.IsNullOrEmpty(languageCode))
        {
            return RedirectToAction("GetContent", new { slug, languageCode = "en" });
        }

        var config = await contentService.GetConfiguration();

        if (config == null)
        {
            return NotFound();
        }

        var redirectionRule = await contentService.GetRedirectionRules(slug);
        if (redirectionRule?.Rules != null && redirectionRule.Rules.TryGetValue(slug, out var destinationSlug))
        {
            return RedirectToAction("GetContent", new { slug = destinationSlug, languageCode });
        }

        var languages = new List<string>();
        if (config is { TranslationEnabled: true })
        {
            languages.AddRange((await translationService.GetLanguages()).Select(l => l.Code));
        }
        if (languages.Count == 0)
        {
            languages.Add("en");
        }
        
        if (!languages.Contains(languageCode))
        {
            return RedirectToAction("GetContent", new { slug, languageCode = "en" });
        }
        
        var page = await contentService.GetPage(slug);

        if (page == null)
        {
            return NotFound();
        }

        return View("Page", page);
    }
}