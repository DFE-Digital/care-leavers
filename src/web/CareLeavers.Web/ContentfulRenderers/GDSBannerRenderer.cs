using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSBannerRenderer(IServiceProvider serviceProvider) : GDSRazorContentRenderer(serviceProvider)
{
    public override bool SupportsContent(IContent content)
    {
        if (content is EntryStructure { NodeType: "embedded-entry-block" } entryStructure)
        {
            if (entryStructure.Data.Target is Banner)
            {
                return true;
            }
        }

        return content is Banner;
    }

    public override Task<string> RenderAsync(IContent content)
    {
        Banner? banner;
        if (content is Banner)
        {
            banner = content as Banner;
        }
        else
        {
            banner = (content as EntryStructure)?.Data.Target as Banner;
        }

        return RenderToString("Shared/Banner", banner);
    }
}