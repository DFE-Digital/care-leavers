using Contentful.Core.Models;
using GovUk.Frontend.AspNetCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSParagraphRenderer(ContentRendererCollection rendererCollection) : IContentRenderer
{
    public bool SupportsContent(IContent content) => content is Paragraph;

    public async Task<string> RenderAsync(IContent content)
    {
        var paragraph = content as Paragraph;
        var tb = new TagBuilder("p");
        tb.AddCssClass("govuk-body");

        foreach (var subContent in paragraph?.Content ?? [])
        {
            var renderer = rendererCollection.GetRendererForContent(subContent);
            tb.InnerHtml.AppendHtml(await renderer.RenderAsync(subContent));
        }

        return tb.ToHtmlString();
    }

    public int Order { get; set; } = 10;
}