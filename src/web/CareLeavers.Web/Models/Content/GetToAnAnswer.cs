using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CareLeavers.Web.Models.Content;

public class GetToAnAnswer : ContentfulContent
{
    public static string ContentType { get; } = "getToAnAnswer";
    
    public string? Title { get; set; }
    
    public string? questionnaireSlug { get; set; }
    
    public string? BaseUrl { get; set; }
}