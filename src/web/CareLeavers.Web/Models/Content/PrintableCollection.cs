using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class PrintableCollection : ContentfulContent
{
    
    public static string ContentType { get; } = "printableBooklet";

    public required string Title { get; set; }
    
    public required string Identifier { get; set; }
    
    public Document? Summary { get; set; }

    public List<Page> Content { get; set; } = [];
    
}