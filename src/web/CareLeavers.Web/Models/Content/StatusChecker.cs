namespace CareLeavers.Web.Models.Content;

public class StatusChecker : ContentfulContent
{
    public static string ContentType { get; } = "statusChecker";
    
    public string? Title { get; set; }
    
    public string? InitialQuestion { get; set; }
    
    public List<AnswerEntity>? Answers { get; set; }
    
    public Page? Page { get; set; }
}