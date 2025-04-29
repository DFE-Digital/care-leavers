using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class ExternalAgency : ContentfulContent
{
    public static string ContentType { get; } = "externalAgency";
    public string? Name { get; set; }
    public Document? Content { get; set; }
    public Asset? Logo { get; set; }
}