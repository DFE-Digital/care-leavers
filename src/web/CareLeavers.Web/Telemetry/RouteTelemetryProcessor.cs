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

        if (languageCode != null || slug != null)
        {
            // Add the language, if we have one, otherwise default to "en"
            string route = $"/{languageCode ?? "en"}";
            
            // Add the slug
            if (slug != null)
            {
                route += $"/{slug}";
            } else if (!string.IsNullOrEmpty(httpContext.Request.Path.Value))
            {
                var paths = httpContext.Request.Path.Value.Split('/');
                if (paths.LastOrDefault() != null)
                {
                    route += $"/{paths.Last()}";
                }
            }
            activity.SetTag("http.route", route);
        }

        base.OnEnd(activity);
    }
}