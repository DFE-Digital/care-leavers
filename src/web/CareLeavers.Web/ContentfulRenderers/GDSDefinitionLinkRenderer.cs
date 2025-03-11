using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;
using GovUk.Frontend.AspNetCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSDefinitionLinkRenderer() : IContentRenderer
{
    public bool SupportsContent(IContent content)
    {
        if (content is EntryStructure { NodeType: "embedded-entry-inline" } entryStructure)
        {
            if (entryStructure.Data.Target is DefinitionLink)
            {
                return true;
            }
        }

        return content is DefinitionLink;

    }

    public async Task<string> RenderAsync(IContent content)
    {
        var tb = new TagBuilder("a");
        tb.AddCssClass("govuk-link");
        DefinitionLink? link = null;
        if (content is EntryStructure { NodeType: "embedded-entry-inline" } entryStructure)
        {
            if (entryStructure.Data.Target is DefinitionLink)
            {
                link = entryStructure.Data.Target as DefinitionLink;
            }
        }
        else
        {
            link = content as DefinitionLink;
        }

        if (link != null)
        {

            // Get page slug
            var slug = link.Page.Slug;

            // Get sanitised id for anchor
            var anchor = TagBuilder.CreateSanitizedId(link.DefinitionBlock.Title, "-");
            
            // Set link
            tb.Attributes["href"] = $"/{slug}#{anchor}";

            // Use the title from the link
            tb.InnerHtml.Append(link.Title);
            
        }

        return tb.HasInnerHtml ? tb.ToHtmlString() : string.Empty;
    }

    public int Order { get; set; } = 10;
}