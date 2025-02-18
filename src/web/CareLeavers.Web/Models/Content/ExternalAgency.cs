using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class ExternalAgency : ContentfulContent
{
    public static string ContentType { get; } = "externalAgency";
    public string? Name { get; set; }
    public string? Url { get; set; }
    public Asset? Logo { get; set; }
    public string? Description { get; set; }
    public string? Call { get; set; }
    public string? OpeningTimes { get; set; }
    public bool Free { get; set; }
    public string? Accessibility { get; set; }
}