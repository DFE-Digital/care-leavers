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
    
    [Route("{languageCode}/pages/error")]
    [Translation(HardcodedSlug="pages/error")]
    public IActionResult Error(int statusCode)
    {
        if (statusCode == 404)
        {
            return PageNotFound();
        }
        return View();
    }
    
    [Route("{languageCode}/pages/service-unavailable")]
    [Translation(HardcodedSlug="pages/service-unavailable")]
    public IActionResult ServiceUnavailable()
    {
        return View();
    }
    
    [Route("{languageCode}/pages/page-not-found")]
    [Translation(HardcodedSlug="pages/page-not-found")]
    public IActionResult PageNotFound()
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        var result = View("PageNotFound");
        result.StatusCode = StatusCodes.Status404NotFound;
        return result;
    }
    
    [HttpPost("/pages/cookie-policy")]
    [HttpPost("/en/pages/cookie-policy")]
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