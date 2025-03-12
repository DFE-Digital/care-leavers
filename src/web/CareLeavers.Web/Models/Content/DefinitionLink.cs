namespace CareLeavers.Web.Models.Content;

public class DefinitionLink : ContentfulContent
{
    public static string ContentType { get; } = "definitionLink";
    
    public required string Title { get; set; }
    
    public required Definition Definition { get; set; }
    
    public required Page Page { get; set; }
    
}