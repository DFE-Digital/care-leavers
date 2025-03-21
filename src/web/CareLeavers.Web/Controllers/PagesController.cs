using CareLeavers.Web.Filters;
using CareLeavers.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CareLeavers.Web.Controllers;

public class PagesController : Controller
{
    [HttpGet("{languageCode}/pages/privacy-policies")]
    [Translation(HardcodedSlug="pages/privacy-policies")]
    public IActionResult PrivacyPolicies()
    {
        return View();
    }
    
    [HttpGet("{languageCode}/pages/cookie-policy")]
    [Translation(HardcodedSlug="pages/cookie-policy")]
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
    
    [HttpGet("{languageCode}/pages/error")]
    [HttpPost("{languageCode}/pages/error")]
    public IActionResult Error(int statusCode, string? languageCode)
    {
        if (statusCode == 404)
        {
            return View("PageNotFound");
        }
        return View();
    }
    
    [HttpGet("{languageCode}/pages/page-not-found")]
    [Translation(HardcodedSlug="pages/page-not-found")]
    public IActionResult PageNotFound()
    {
        return View();
    }
    
    [HttpPost("/pages/cookie-policy")]
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