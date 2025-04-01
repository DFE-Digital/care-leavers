using CareLeavers.Web.Filters;
using CareLeavers.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CareLeavers.Web.Controllers;

public class PagesController : Controller
{
    [HttpGet("{languageCode}/privacy-policies")]
    [Translation(HardcodedSlug="privacy-policies")]
    public IActionResult PrivacyPolicies()
    {
        return View();
    }
    
    [HttpGet("{languageCode}/cookie-policy")]
    [Translation(HardcodedSlug="cookie-policy")]
    public IActionResult CookiePolicy()
    {
        var consent = HttpContext.Features.Get<ITrackingConsentFeature>() ??
            throw new InvalidOperationException("ITrackingConsentFeature is not available.");

        var vm = new CookiePolicyModel
        {
            AcceptCookies = consent.CanTrack
        };
        
        return View(vm);
    }
    
    [Route("{languageCode}/error")]
    [Translation(HardcodedSlug="error")]
    public IActionResult Error(int statusCode)
    {
        if (statusCode == 404)
        {
            return PageNotFound();
        }
        return View();
    }
    
    [Route("{languageCode}/service-unavailable")]
    [Translation(HardcodedSlug="service-unavailable")]
    public IActionResult ServiceUnavailable()
    {
        return View();
    }
    
    [Route("{languageCode}/page-not-found")]
    [Translation(HardcodedSlug="page-not-found")]
    public IActionResult PageNotFound()
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        var result = View("PageNotFound");
        result.StatusCode = StatusCodes.Status404NotFound;
        return result;
    }
    
    [Route("{languageCode}/accessibility-statement")]
    [Translation(HardcodedSlug="accessibility-statement")]
    public IActionResult AccessibilityStatement()
    {
        return View();
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