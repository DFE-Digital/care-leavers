using CareLeavers.Web.Models.Enums;
using CareLeavers.Web.Models.ViewModels;
using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class CallToAction : ContentfulContent
{
    public static string ContentType { get; } = "callToAction";
    
    public string? Title { get; set; }

    public CallToActionSize? Size { get; set; } = CallToActionSize.Small;

    public Document? Content { get; set; }

    public string? CallToActionText { get; set; }
    
    public Page? InternalDestination { get; set; }
    
    public string? ExternalDestination { get; set; }


}