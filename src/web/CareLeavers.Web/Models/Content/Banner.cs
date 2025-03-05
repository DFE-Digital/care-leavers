using CareLeavers.Web.Models.Enums;
using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class Banner : ContentfulContent
{
    public static string ContentType { get; } = "banner";
    
    public string? Title { get; set; }
    
    public string? Text { get; set; }
    
    public Asset? Image { get; set; }
    
    public string? LinkText { get; set; }
    
    public Page? Link { get; set; }
    
    public BackgroundColour? Background { get; set; }


}