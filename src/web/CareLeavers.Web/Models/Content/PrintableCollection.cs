using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class PrintableCollection : ContentfulContent
{
    
    public const string ContentType = "printableCollection";

    public required string Title { get; set; }
    
    public required string Identifier { get; set; }
    
    public Document? Summary { get; set; }

    public List<Page> Content { get; set; } = [];
    
}