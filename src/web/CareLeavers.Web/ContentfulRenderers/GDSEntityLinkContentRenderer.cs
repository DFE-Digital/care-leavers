using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;
using GovUk.Frontend.AspNetCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSEntityLinkContentRenderer(ContentRendererCollection rendererCollection) : IContentRenderer
{
    public bool SupportsContent(IContent content)
    {
        if (content is EntryStructure et)
        {
            return et.NodeType switch
            {
                "entry-hyperlink" or "inline-entry-hyperlink" => true,
                _ => false
            };
        }

        return false;
    }

    public async Task<string> RenderAsync(IContent content)
    {
        var link = (content as EntryStructure);
        var tb = new TagBuilder("a");
        tb.AddCssClass("govuk-hyperlink");
        switch (link?.Data.Target)
        {
            case Page p:
                tb.Attributes["href"] = p.Slug;
                foreach (var subContent in link.Content)
                {
                    var renderer = rendererCollection.GetRendererForContent(subContent);
                    tb.InnerHtml.AppendHtml(await renderer.RenderAsync(subContent));
                }
                break;
        }

        return tb.HasInnerHtml ? tb.ToHtmlString() : string.Empty;
    }

    public int Order { get; set; } = 10;
}