using CareLeavers.Web.Models.Enums;
using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class Page : ContentfulContent
{
    public static string ContentType { get; } = "page";
    
    public Page? Parent { get; set; }
    
    public string? SeoTitle { get; set; }
    
    public string? SeoDescription { get; set; }
    
    public Asset? SeoImage { get; set; }
    
    public PageWidth Width { get; set; }
    
    public PageType? Type { get; set; }
    
    public string? Title { get; set; }
    
    public string? Slug { get; set; }
    
    
    public bool ShowBreadcrumb { get; set; } = true;

    public bool ShowContentsBlock { get; set; } = false;
    
    public HeadingType[] ContentsHeadings { get; set; } = [ ];
    
    public bool ShowLastUpdated { get; set; }
    
    public bool ShowShareThis { get; set; }
    
    public bool ShowFooter { get; set; }
    
    public Document? Header { get; set; }
    
    public Document? MainContent { get; set; }
    
    public Document? SecondaryContent { get; set; }
}
