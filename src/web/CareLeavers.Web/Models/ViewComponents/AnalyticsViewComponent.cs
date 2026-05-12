using CareLeavers.Web.Configuration;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CareLeavers.Web.Models.ViewComponents;

public sealed class AnalyticsViewComponent(IOptions<ScriptOptions> options, IHttpContextAccessor accessor)
    : ViewComponent
{
    public string? GoogleAnalyticsTag { get; set; }
    public string? GoogleTagManagerTag { get; set; }
    public string? MicrosoftClarityTag { get; set; }
    public bool Consent { get; set; }

    public IViewComponentResult Invoke(bool? consent = null)
    {
        ITrackingConsentFeature? consentFeature = accessor.HttpContext?.Features.Get<ITrackingConsentFeature>();

        GoogleAnalyticsTag = options.Value.Ga4;
        GoogleTagManagerTag = options.Value.GTM;
        MicrosoftClarityTag = options.Value.Clarity;
        Consent = consent ?? consentFeature?.HasConsent ?? false;

        return View(this);
    }
}