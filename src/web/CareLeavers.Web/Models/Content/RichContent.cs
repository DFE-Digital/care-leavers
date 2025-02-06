using CareLeavers.Web.Models.Enums;
using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class RichContent : ContentfulContent
{
    public static string ContentType { get; } = "richContent";
    
    public string? Description { get; set; }
    
    public BackgroundColour? Background { get; set; }
    
    public ContentWidth? Width { get; set; }
    
    public Document? Content { get; set; }
}