using CareLeavers.Web.Contentful;
using CareLeavers.Web.Filters;
using CareLeavers.Web.Models;
using CareLeavers.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CareLeavers.Web.Controllers;

public class PagesController(IContentService contentService) : Controller
{
    [HttpGet("{languageCode}/privacy-policies")]
    [Translation(HardcodedSlug="privacy-policies")]
    public async Task<IActionResult> PrivacyPolicies()
    {
        var page = await contentService.GetPage("privacy-policies");
        
        return View(page ?? new());
    }
    
    [HttpGet("{languageCode}/cookie-policy")]
    [Translation(HardcodedSlug="cookie-policy")]
    public async Task<IActionResult> CookiePolicy()
    {
        var consent = HttpContext.Features.Get<ITrackingConsentFeature>() ??
            throw new InvalidOperationException("ITrackingConsentFeature is not available.");
        
        var page = await contentService.GetPage("cookie-policy");

        var vm = new CookiePolicyModel
        {
            AcceptCookies = consent.CanTrack,
            Page = page ?? new(),
        };
        
        return View(vm);
    }
    
    [Route("{languageCode}/error")]
    [Translation(HardcodedSlug="error")]
    public async Task<IActionResult> Error(int statusCode)
    {
        if (statusCode == 404)
        {
            return await PageNotFound();
        }
        
        var page = await contentService.GetPage($"error-{statusCode}");
        page ??= await contentService.GetPage("error");

        var viewModel = new ErrorViewModel
        {
            Page = page ?? new (),
        };
        
        return View(viewModel);
    }
    
    [Route("{languageCode}/service-unavailable")]
    [Translation(HardcodedSlug="service-unavailable")]
    public async Task<IActionResult> ServiceUnavailable()
    {
        var page = await contentService.GetPage("service-unavailable");
        
        return View(page ?? new());
    }
    
    [Route("{languageCode}/page-not-found")]
    [Translation(HardcodedSlug="page-not-found")]
    public async Task<IActionResult> PageNotFound()
    {
        var page = await contentService.GetPage("page-not-found");

        Response.StatusCode = StatusCodes.Status404NotFound;
        var result = View("PageNotFound", page ?? new ());
        result.StatusCode = StatusCodes.Status404NotFound;
        return result;
    }
    
    [Route("{languageCode}/accessibility-statement")]
    [Translation(HardcodedSlug="accessibility-statement")]
    public async Task<IActionResult> AccessibilityStatement()
    {
        var page = await contentService.GetPage("accessibility-statement");
        
        return View(page ?? new ());
    }
    
    
    [HttpPost("/{languageCode}/cookie-policy")]
    public IActionResult PostCookiePolicy(
        [FromForm] CookiePolicyModel cookiePolicyModel,
        [FromServices] IOptions<CookiePolicyOptions> cookiePolicyOptions)
    {
        var cookieOptions = cookiePolicyOptions.Value.ConsentCookie.Build(HttpContext);
        var cookieValue = cookiePolicyModel.AcceptCookies ? cookiePolicyOptions.Value.ConsentCookieValue : "no";

        HttpContext.Response.Cookies.Append(
            cookiePolicyOptions.Value.ConsentCookie.Name ?? string.Empty,
            cookieValue,
            cookieOptions);

        cookiePolicyModel.ShowSuccessBanner = true;
        
        return View("CookiePolicy", cookiePolicyModel);
    }
}