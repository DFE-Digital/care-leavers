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
        
        if (slug != null)
        {
            activity.SetTag("http.route", $"/{slug}");
        }
        
        base.OnEnd(activity);
    }
}