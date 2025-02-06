using Contentful.AspNetCore.Authoring;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CareLeavers.Web.ContentfulRenderers;

public abstract class GDSRazorContentRenderer(
    IServiceProvider serviceProvider)
    : RazorContentRenderer(
        serviceProvider.GetRequiredService<IRazorViewEngine>(),
        serviceProvider.GetRequiredService<ITempDataProvider>(),
        serviceProvider)
{
    public override string Render(IContent content)
    {
        var result = RenderAsync(content);
        result.Wait();
        return result.Result;
    }
}