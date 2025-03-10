using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSDefinitionBlockRenderer(IServiceProvider serviceProvider) : GDSRazorContentRenderer(serviceProvider)
{
    public override bool SupportsContent(IContent content)
    {
        if (content is EntryStructure { NodeType: "embedded-entry-block" } entryStructure)
        {
            if (entryStructure.Data.Target is DefinitionBlock)
            {
                return true;
            }
        }

        return content is DefinitionBlock;
    }

    public override Task<string> RenderAsync(IContent content)
    {
        DefinitionBlock? definitionBlock;
        if (content is DefinitionBlock)
        {
            definitionBlock = content as DefinitionBlock;
        }
        else
        {
            definitionBlock = (content as EntryStructure)?.Data.Target as DefinitionBlock;
        }

        return RenderToString("Shared/DefinitionBlock", definitionBlock);
    }
}