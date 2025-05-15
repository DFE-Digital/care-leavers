using CareLeavers.Web.Models.Enums;
using CareLeavers.Web.Models.ViewModels;
using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class Button : ContentfulContent
{
    public static string ContentType { get; } = "button";
    
    public required string Title { get; set; }

    public required ButtonType Type { get; set; } = ButtonType.Default;

    public required string Text { get; set; }
    
    public Page? InternalDestination { get; set; }
    
    public string? ExternalDestination { get; set; }

}