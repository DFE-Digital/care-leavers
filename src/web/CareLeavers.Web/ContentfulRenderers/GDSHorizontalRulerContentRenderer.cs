using Contentful.Core.Models;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSHorizontalRulerContentRenderer : IContentRenderer
{
    public bool SupportsContent(IContent content) => content is HorizontalRuler;

    public Task<string> RenderAsync(IContent content)
    {
        return Task.FromResult("<hr class=\"govuk-section-break govuk-section-break--l govuk-section-break--visible\">");
    }

    public int Order { get; set; } = 10;
}