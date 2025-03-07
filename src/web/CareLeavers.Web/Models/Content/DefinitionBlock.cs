namespace CareLeavers.Web.Models.Content;

public class DefinitionBlock : ContentfulContent
{
    public static string ContentType { get; } = "definitionBlock";
    
    public required string Title { get; set; }

    public bool ShowTitle { get; set; } = false;
    
    public required DefinitionContent DefinitionContent { get; set; }
    
}