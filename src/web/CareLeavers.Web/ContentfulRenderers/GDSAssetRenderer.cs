using System.Text;
using Contentful.Core.Models;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSAssetRenderer(ContentRendererCollection rendererCollection) : IContentRenderer
{
    public bool SupportsContent(IContent content) => content is AssetStructure;

    public async Task<string> RenderAsync(IContent content)
    {
        var assetStructure = content as AssetStructure;
        var asset = assetStructure!.Data.Target;
        var nodeType = assetStructure.NodeType;
        var sb = new StringBuilder();
        if (nodeType != "asset-hyperlink" && asset.File?.ContentType != null &&
            asset.File.ContentType.ToLower().Contains("image"))
        {
            sb.Append($"<img class=\"full-width-image\" src=\"{asset.File.Url}\" alt=\"{asset.Description}\" />");
        }
        else
        {
            var url = asset.File?.Url ?? "";
            sb.Append(string.IsNullOrEmpty(url) ? "<a class=\"govuk-link\">" : $"<a class=\"govuk-link\" href=\"{url}\">");

            if (assetStructure.Content != null && assetStructure.Content.Any())
            {
                foreach (var subContent in assetStructure.Content)
                {
                    var renderer = rendererCollection.GetRendererForContent(subContent);
                    sb.Append(await renderer.RenderAsync(subContent));
                }
            }
            else
            {
                sb.Append(asset.Title);
            }

            sb.Append("</a>");
        }

        return sb.ToString();
    }

    public int Order { get; set; } = 10;
}