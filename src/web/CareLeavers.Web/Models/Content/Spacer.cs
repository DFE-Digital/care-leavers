using CareLeavers.Web.Models.Enums;

namespace CareLeavers.Web.Models.Content;

public class Spacer : ContentfulContent
{
    public static string ContentType { get; } = "spacer";
    
    public required string Title { get; set; }

    public required SpacerSize Size { get; set; } = SpacerSize.S;

}