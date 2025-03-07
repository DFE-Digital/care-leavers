namespace CareLeavers.Web.Models.Content;

public class DefinitionLink : ContentfulContent
{
    public static string ContentType { get; } = "definitionLink";
    
    public required string Title { get; set; }
    
    public required DefinitionBlock DefinitionBlock { get; set; }
    
    public required Page Page { get; set; }
    
}