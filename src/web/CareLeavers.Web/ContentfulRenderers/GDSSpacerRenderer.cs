using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Models.Enums;
using Contentful.Core.Models;

namespace CareLeavers.Web.ContentfulRenderers;

/// <summary>
/// A renderer for a paragraph.
/// </summary>
public class GDSSpacerRenderer() : IContentRenderer
{
    public bool SupportsContent(IContent content)
    {
        if (content is EntryStructure)
        {
            var structure = content as EntryStructure;
            if (structure?.NodeType == "embedded-entry-block")
            {
                if (structure.Data.Target is Spacer)
                    return true;
            }
        }

        return content is Spacer;
    }

    public Task<string> RenderAsync(IContent content)
    {
        Spacer? spacer;
        if (content is Spacer)
        {
            spacer = content as Spacer;
        }
        else
        {
            spacer = (content as EntryStructure)?.Data.Target as Spacer;
        }

        if (spacer == null)
        {
            return Task.FromResult(string.Empty);
        }
        
        var size = "";

        switch (spacer.Size)
        {
            case SpacerSize.L:
                size = " govuk-section-break--xl";
                break;
            case SpacerSize.M:
                size = " govuk-section-break--l";
                break;
            case SpacerSize.S:
                size = " govuk-section-break--m";
                break;
        }

        return Task.FromResult($"<hr class=\"govuk-section-break{size}\"/>");
    }

    public int Order { get; set; } = 10;
}