using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class Page : ContentfulContent
{
    public string? Title { get; set; }
    
    public string? Slug { get; set; }
    
    public Document? Content { get; set; }
}