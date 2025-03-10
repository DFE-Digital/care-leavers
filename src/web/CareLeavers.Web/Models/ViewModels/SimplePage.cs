using CareLeavers.Web.Models.Content;

namespace CareLeavers.Web.Models.ViewModels;

public class SimplePage
{
    public string? Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Slug { get; set; }
    
    public string? Parent { get; set; }

    public SimplePage(Page page)
    {
        Id = page.Sys.Id;
        Title = page.Title;
        Slug = page.Slug;
    }
    
    public SimplePage(string id, string title, string slug, string parent)
    {
        Id = id;
        Title = title;
        Slug = slug;
        Parent = parent;
    }
}