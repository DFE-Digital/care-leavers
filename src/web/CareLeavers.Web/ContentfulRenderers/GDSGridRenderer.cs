using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSGridRenderer(IServiceProvider serviceProvider) : GDSRazorContentRenderer(serviceProvider)
{
    public override bool SupportsContent(IContent content)
    {
        if (content is EntryStructure { NodeType: "embedded-entry-block" } entryStructure)
        {
            if (entryStructure.Data.Target is Grid)
            {
                return true;
            }
        }

        return content is Grid;
    }

    public override Task<string> RenderAsync(IContent content)
    {
        Grid? grid;
        if (content is Grid)
        {
            grid = content as Grid;
        }
        else
        {
            grid = (content as EntryStructure)?.Data.Target as Grid;
        }

        return RenderToString("Shared/Grid", grid);
    }
}