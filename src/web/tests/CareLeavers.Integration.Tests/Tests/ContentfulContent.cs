namespace CareLeavers.Integration.Tests.Tests;

public class ContentfulContent
{
    public string? Id { get; set; }
    
    public string? Slug { get; set; }
    
    public string? ContentType { get; set; }
    
    public required string Content { get; set; }
}