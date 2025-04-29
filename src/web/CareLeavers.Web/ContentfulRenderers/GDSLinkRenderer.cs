using CareLeavers.Web.Contentful;
using CareLeavers.Web.Models.Content;
using Contentful.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareLeavers.Web.ContentfulRenderers;

public class GDSLinkRenderer(ContentRendererCollection rendererCollection, IServiceProvider serviceProvider) : IContentRenderer
{
    public bool SupportsContent(IContent content)
    {
        if (content is EntryStructure et)
        {
            return et.NodeType switch
            {
                "entry-hyperlink" or "inline-entry-hyperlink" => true,
                _ => false
            };
        }

        if (content is Hyperlink)
        {
            return true;
        }

        return false;
    }

    public async Task<string> RenderAsync(IContent content)
    {
        var tb = new TagBuilder("a");
        tb.AddCssClass("govuk-link");
        
        if (content is Hyperlink hyperLink)
        {
            tb.Attributes["href"] = hyperLink.Data.Uri;
            if (hyperLink.Content.Any())
            {
                foreach (var subContent in hyperLink.Content)
                {
                    var renderer = rendererCollection.GetRendererForContent(subContent);
                    tb.InnerHtml.AppendHtml(await renderer.RenderAsync(subContent));
                }
            }
            else
            {
                tb.InnerHtml.Append(hyperLink.Data.Title);
            }
        }
        else
        {

            var link = (content as EntryStructure);
            
            if (link != null)
            {
                foreach (var subContent in link.Content)
                {
                    var renderer = rendererCollection.GetRendererForContent(subContent);
                    tb.InnerHtml.AppendHtml(await renderer.RenderAsync(subContent));
                }
            }
            
            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            var httpContext = httpContextAccessor?.HttpContext;
            var helper = serviceProvider.GetService<LinkGenerator>();
            
            if (httpContext != null && helper != null)
            {
                var routeData = httpContext.GetRouteData();
                routeData.Values.TryGetValue("languageCode", out var languageCode);
                if (languageCode == null)
                {
                    languageCode = "en";
                }

                switch (link?.Data.Target)
                {
                    case Page p:
                        tb.Attributes["href"] = helper.GetPathByAction("GetContent", "Contentful",
                            values: new { slug = p.Slug, languageCode });
                        break;

                    case PrintableCollection pc:
                        tb.Attributes["href"] = helper.GetPathByAction("DownloadPdf", "Print",
                            values: new { identifier = pc.Identifier, languageCode });
                        tb.Attributes["rel"] = "nofollow";
                        
                        break;
                }
            }
            else
            {
                switch (link?.Data.Target)
                {
                    case Page p:
                        tb.Attributes["href"] = p.Slug;
                        break;

                    case PrintableCollection pc:
                        tb.Attributes["href"] = $"/pdf/{pc.Identifier}";
                        tb.Attributes["rel"] = "nofollow";
                        break;
                }
            }

            
        }

        return tb.HasInnerHtml ? tb.ToHtmlString() : string.Empty;
    }

    public int Order { get; set; } = 10;
}