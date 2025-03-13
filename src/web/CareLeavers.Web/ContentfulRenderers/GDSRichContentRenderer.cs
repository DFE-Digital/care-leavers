using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSRichContentRenderer(IServiceProvider serviceProvider) : GDSRazorContentRenderer(serviceProvider)
{
    public override bool SupportsContent(IContent content)
    {
        if (content is EntryStructure)
        {
            var structure = content as EntryStructure;
            if (structure?.NodeType == "embedded-entry-block")
            {
                if (structure.Data.Target is RichContentBlock)
                    return true;
            }
        }

        return content is RichContentBlock;
    }

    public override Task<string> RenderAsync(IContent content)
    {
        RichContentBlock? block;
        if (content is RichContentBlock)
        {
            block = content as RichContentBlock;
        }
        else
        {
            block = (content as EntryStructure)?.Data.Target as RichContentBlock;
        }

        return RenderToString("Shared/RichContent/Block", block);
    }
}