using System.Text.Json.Serialization;
using CareLeavers.Web.Models.Content;

namespace CareLeavers.Web.Models.ViewModels;

public class SimplePage
{
    public string? Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Slug { get; set; }
    
    public string? Parent { get; set; }
    
}