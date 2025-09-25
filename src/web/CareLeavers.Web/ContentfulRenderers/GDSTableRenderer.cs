using System.Text.Encodings.Web;
using CareLeavers.Web.Contentful;
using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSTableRenderer : IContentRenderer
{
    public bool SupportsContent(IContent content)
    {
        if (content is EntryStructure et)
        {
            return et.NodeType switch
            {
                "entry-table" or "inline-entry-table" => true,
                _ => false
            };
        }

        if (content is Table) 
        {
            return true;
        }

        return false;
    }

    public async Task<string> RenderAsync(IContent content)
    {
        var tb = new TagBuilder("table");
        tb.AddCssClass("govuk-table");

        if (content is Table table)
        {
            var theadTag = new TagBuilder("thead");
            theadTag.AddCssClass("govuk-table__head");
            
            foreach (var th in table.Content.OfType<TableHeader>())
            {
                // Extract plain text from cell
                var cellText = string.Join("", th.Content
                    .OfType<Paragraph>()
                    .SelectMany(p => p.Content.OfType<Text>())
                    .Select(t => t.Value));

                var thTag = new TagBuilder("th");
                thTag.AddCssClass("govuk-table__header");
                thTag.InnerHtml.Append(cellText);

                theadTag.InnerHtml.AppendHtml(thTag);
            }
            
            var tbodyTag = new TagBuilder("tbody");
            tbodyTag.AddCssClass("govuk-table__body");

            foreach (var row in table.Content.OfType<TableRow>())
            {
                var trTag = new TagBuilder("tr");

                foreach (var cell in row.Content.OfType<TableCell>())
                {
                    // Extract plain text from cell
                    var cellText = string.Join("", cell.Content
                        .OfType<Paragraph>()
                        .SelectMany(p => p.Content.OfType<Text>())
                        .Select(t => t.Value));

                    var tdTag = new TagBuilder("td");
                    tdTag.AddCssClass("govuk-table__cell");
                    tdTag.InnerHtml.Append(cellText);

                    trTag.InnerHtml.AppendHtml(tdTag);
                }

                tbodyTag.InnerHtml.AppendHtml(trTag);
            }

            tb.InnerHtml.AppendHtml(tbodyTag);
        }
        
        return tb.HasInnerHtml ? tb.ToHtmlString() : string.Empty;
    }

    public int Order { get; set; } = 10;
}