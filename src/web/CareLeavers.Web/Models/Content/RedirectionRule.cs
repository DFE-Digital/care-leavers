namespace CareLeavers.Web.Models.Content;

public class RedirectionRule : ContentfulContent
{
    public static string ContentType { get; } = "redirectionRule";
    
    public string FromSlug { get; set; } = string.Empty;
    
    public string ToSlug { get; set; } = string.Empty;
}