using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class Card : ContentfulContent
{
    public static string ContentType { get; } = "card";
    
    public string? Title { get; set; }
    
    public string? Text { get; set; }
    
    public Asset? Image { get; set; }
    
    public Page? Link { get; set; }

    public List<string> Types { get; set; } = new();

    public int Position { get; set; } = 0;
}