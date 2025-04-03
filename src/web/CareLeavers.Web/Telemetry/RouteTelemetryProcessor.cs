using System.Diagnostics;
using OpenTelemetry;

namespace CareLeavers.Web.Telemetry;

public class RouteTelemetryProcessor(IHttpContextAccessor httpContextAccessor) : BaseProcessor<Activity>
{
    public override void OnEnd(Activity activity)
    {
        var httpContext = httpContextAccessor.HttpContext;
        
        if (httpContext == null)
        {
            base.OnEnd(activity);
            return;
        }
        
        var routeData = httpContext.GetRouteData();
        routeData.Values.TryGetValue("slug", out var slug);
        routeData.Values.TryGetValue("languageCode", out var languageCode);

        var route = activity.GetTagItem("http.route")?.ToString() ?? string.Empty;

        // Add the slug
        if (slug != null)
        {
            route = route.Replace("{slug}", slug.ToString());
        }
        
        // Add the language, if we have one, otherwise default to "en"
        route = route.Replace("{languageCode}", languageCode?.ToString() ?? "en");
        
        if (!string.IsNullOrEmpty(route))
        {
            activity.SetTag("http.route", route);
        }
        
        base.OnEnd(activity);
    }
}