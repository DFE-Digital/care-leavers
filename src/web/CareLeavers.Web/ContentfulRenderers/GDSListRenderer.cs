using CareLeavers.Web.Contentful;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareLeavers.Web.ContentfulRenderers;

/// <summary>
/// A renderer for a list.
/// </summary>
public class GDSListRenderer(ContentRendererCollection rendererCollection) : IContentRenderer
{

    /// <summary>
    /// Whether or not this renderer supports the provided content.
    /// </summary>
    /// <param name="content">The content to evaluate.</param>
    /// <returns>Returns true if the content is a list, otherwise false.</returns>
    public bool SupportsContent(IContent content)
    {
        return content is List;
    }
    
    /// <summary>
    /// Renders the content asynchronously.
    /// </summary>
    /// <param name="content">The content to render.</param>
    /// <returns>The list as a ul or ol HTML string.</returns>
    public async Task<string> RenderAsync(IContent content)
    {
        var list = content as List;
        if (list == null)
        {
            return string.Empty;
        }
        
        var listTagType = "ul";
        var listClass = "govuk-list govuk-list--bullet govuk-list--spaced";
        if (list.NodeType == "ordered-list")
        {
            listTagType = "ol";
            listClass = "govuk-list govuk-list--number govuk-list--spaced";
        }

        var tb = new TagBuilder(listTagType);
        tb.AddCssClass(listClass);
            

        foreach (var subContent in list.Content)
        {
            var renderer = rendererCollection.GetRendererForContent(subContent);
            tb.InnerHtml.AppendHtml(await renderer.RenderAsync(subContent));
        }


        return tb.ToHtmlString();
    }
    
    public int Order { get; set; } = 10;

}