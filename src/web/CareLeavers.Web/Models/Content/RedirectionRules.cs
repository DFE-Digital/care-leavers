namespace CareLeavers.Web.Models.Content;

public class RedirectionRules : ContentfulContent
{
    public static string ContentType { get; } = "redirectionRules";

    public string Title { get; set; } = string.Empty;
    
    public Dictionary<string, string> Rules { get; set; } = new();
}