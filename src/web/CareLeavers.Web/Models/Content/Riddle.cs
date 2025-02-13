namespace CareLeavers.Web.Models.Content;

public class Riddle : ContentfulContent
{
    public static string ContentType { get; } = "riddle";
    
    public string? Title { get; set; }
    
    public string? RiddleId { get; set; }
}