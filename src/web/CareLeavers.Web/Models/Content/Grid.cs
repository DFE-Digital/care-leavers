using CareLeavers.Web.Models.Enums;
using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class Grid : ContentfulContent
{
    public static string ContentType { get; } = "grid";
    
    public string? Title { get; set; }
    
    public GridType? GridType { get; set; }
    
    public List<IContent>? Content { get; set; }

    public string? CssClass { get; set; } = "govuk-grid-column-full";
}