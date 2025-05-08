using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class ContentfulContent : IContent
{
    public SystemProperties Sys { get; set; } = new();
    
    public ContentfulMetadata? ContentfulMetadata { get; set; }
    
    public DateTime Fetched { get; set; } = DateTime.Now;
}