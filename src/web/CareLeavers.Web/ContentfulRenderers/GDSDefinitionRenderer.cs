using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSDefinitionRenderer(IServiceProvider serviceProvider) : GDSRazorContentRenderer(serviceProvider)
{
    public override bool SupportsContent(IContent content)
    {
        if (content is EntryStructure { NodeType: "embedded-entry-block" } entryStructure)
        {
            if (entryStructure.Data.Target is Definition)
            {
                return true;
            }
        }

        return content is Definition;
    }

    public override Task<string> RenderAsync(IContent content)
    {
        Definition? definition;
        if (content is Definition)
        {
            definition = content as Definition;
        }
        else
        {
            definition = (content as EntryStructure)?.Data.Target as Definition;
        }

        return RenderToString("Shared/Definition", definition);
    }
}