using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSButtonRenderer(IServiceProvider serviceProvider) : GDSRazorContentRenderer(serviceProvider)
{
    public override bool SupportsContent(IContent content)
    {
        if (content is EntryStructure { NodeType: "embedded-entry-block" } embeddedEntryBlock)
        {
            if (embeddedEntryBlock.Data.Target is Button)
            {
                return true;
            }
        }

        return content is Button;
    }

    public override Task<string> RenderAsync(IContent content)
    {
       
        Button? button;
        if (content is Button)
        {
            button = content as Button;
        }
        else
        {
            button = (content as EntryStructure)?.Data.Target as Button;
        }

        return button != null ? RenderToString("Shared/Button", button) : Task.FromResult(string.Empty);
    }
}