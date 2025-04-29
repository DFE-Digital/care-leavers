using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSCallToActionRenderer(IServiceProvider serviceProvider) : GDSRazorContentRenderer(serviceProvider)
{
    public override bool SupportsContent(IContent content)
    {
        if (content is EntryStructure { NodeType: "embedded-entry-block" } entryStructure)
        {
            if (entryStructure.Data.Target is CallToAction)
            {
                return true;
            }
        }

        return content is CallToAction;
    }

    public override Task<string> RenderAsync(IContent content)
    {
        CallToAction? cta;
        if (content is Banner)
        {
            cta = content as CallToAction;
        }
        else
        {
            cta = (content as EntryStructure)?.Data.Target as CallToAction;
        }

        return RenderToString("Shared/CallToAction", cta);
    }
}