namespace CareLeavers.Web.Models.Content;

public class NavigationElement : ContentfulContent
{
    public static string ContentType { get; } = "navigationElement";
    
    public string Title { get; set; } = string.Empty;
    
    public PageLink? Link { get; set; }

    public string Slug => Link?.Slug ?? string.Empty;
}