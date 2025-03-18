using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;

namespace CareLeavers.Web.ContentfulRenderers;

/// <summary>
/// A renderer for a paragraph.
/// </summary>
public class GDSStatusCheckerRenderer (IServiceProvider serviceProvider) : GDSRazorContentRenderer(serviceProvider)
{
    /// <summary>
    /// Whether or not this renderer supports the provided content.
    /// </summary>
    /// <param name="content">The content to evaluate.</param>
    /// <returns>Returns true if the content is a paragraph, otherwise false.</returns>
    public override bool SupportsContent(IContent content)
    {
        if (content is EntryStructure structure)
        {
            if (structure.NodeType == "embedded-entry-block")
            {
                if (structure.Data.Target is StatusChecker)
                {
                    return true;
                }
            }
        }

        return content is StatusChecker;
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
        StatusChecker? checker;
        if (content is StatusChecker)
        {
            checker = content as StatusChecker;
        }
        else
        {
            checker = (content as EntryStructure)?.Data.Target as StatusChecker;
        }

        return RenderToString("StatusChecker", checker);
    }
}