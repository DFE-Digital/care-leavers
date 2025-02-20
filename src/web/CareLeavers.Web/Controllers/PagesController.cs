using CareLeavers.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CareLeavers.Web.Controllers;

[Route("/pages")]
public class PagesController : Controller
{
    [HttpGet("cookie-policy")]
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
    
    [HttpPost("cookie-policy")]
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