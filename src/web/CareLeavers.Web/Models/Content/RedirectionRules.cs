namespace CareLeavers.Web.Models.Content;

public class RedirectionRules : ContentfulContent
{
    public const string ContentType = "redirectionRules";

    public string Title { get; set; } = string.Empty;
    
    public Dictionary<string, string> Rules { get; set; } = new();
}