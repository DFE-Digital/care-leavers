using Contentful.Core.Models;
using GovUk.Frontend.AspNetCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSHeaderRenderer(ContentRendererCollection rendererCollection) : IContentRenderer
{
    private Dictionary<Type, (int size, string tag)> _heading = new()
    {
        { typeof(Heading1), (1, "xl") },
        { typeof(Heading2), (2, "l") },
        { typeof(Heading3), (3, "m") },
        { typeof(Heading4), (4, "s") },
        { typeof(Heading5), (5, "s") },
        { typeof(Heading6), (6, "s") }
    };
    
    public bool SupportsContent(IContent content) =>
        content is Heading1 or Heading2 or Heading3 or Heading4 or Heading5 or Heading6;

    public async Task<string> RenderAsync(IContent content)
    {
        var headingData = _heading[content.GetType()];
        var headingTag = new TagBuilder($"h{headingData.size}");
        headingTag.AddCssClass("govuk-heading-" + headingData.tag);

        var heading = content as IHeading;
        
        foreach (var subContent in heading?.Content ?? [])
        {
            var renderer = rendererCollection.GetRendererForContent(subContent);
            headingTag.InnerHtml.AppendHtml(await renderer.RenderAsync(subContent));
        }
        headingTag.GenerateId(headingTag.InnerHtml.ToHtmlString(), "-");
            
        return headingTag.ToHtmlString();
    }

    public int Order { get; set; } = 10;
}