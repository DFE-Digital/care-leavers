using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class DefinitionContent : ContentfulContent
{
    public static string ContentType { get; } = "definitionContent";
    
    public required string Title { get; set; }
    
    public required Document Content { get; set; }
    
}