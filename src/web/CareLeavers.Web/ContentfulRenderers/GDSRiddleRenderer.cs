using CareLeavers.Web.Models.Content;
using Contentful.AspNetCore.Authoring;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CareLeavers.Web.ContentfulRenderers;

/// <summary>
/// A renderer for a paragraph.
/// </summary>
public class GDSRiddleRenderer(IServiceProvider serviceProvider) : GDSRazorContentRenderer(serviceProvider)
{
    /// <summary>
    /// Whether or not this renderer supports the provided content.
    /// </summary>
    /// <param name="content">The content to evaluate.</param>
    /// <returns>Returns true if the content is a paragraph, otherwise false.</returns>
    public override bool SupportsContent(IContent content)
    {
        if (content is EntryStructure)
        {
            var structure = content as EntryStructure;
            if (structure?.NodeType == "embedded-entry-block")
            {
                if (structure.Data.Target is Riddle)
                    return true;
            }
        }

        return content is Riddle;
    }

    public override string Render(IContent content)
    {
        var result = RenderAsync(content);
        result.Wait();
        return result.Result;
    }

    /// <summary>
    /// Renders the content to an html p-tag.
    /// </summary>
    /// <param name="content">The content to render.</param>
    /// <returns>The p-tag as a string.</returns>
    public override Task<string> RenderAsync(IContent content)
    {
        Riddle? riddle;
        if (content is Riddle)
        {
            riddle = content as Riddle;
        }
        else
        {
            riddle = (content as EntryStructure)?.Data.Target as Riddle;
        }

        return RenderToString("Riddle", riddle);
    }
}