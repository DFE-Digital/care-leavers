namespace CareLeavers.Web.Models.Content;

public class AnswerEntity : ContentfulContent
{
    public static string ContentType { get; } = "answer";
    
    public string? Answer { get; set; }
    
    public string? Description { get; set; }
    
    public PageLink? Target { get; set; }
    
    public int Priority { get; set; }
}