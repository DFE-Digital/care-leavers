namespace CareLeavers.Web.Models.Content;

public class PageLink : ContentfulContent
{
    public string? Title { get; set; }
    
    public PageLink? Parent { get; set; }
    
    public string? Slug { get; set; }

}