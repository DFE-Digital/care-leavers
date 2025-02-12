namespace CareLeavers.ContentfulMigration;

public class Migration
{
    public string Name { get; set; } = string.Empty;
    
    public DateTime AppliedAtUtc { get; set; }
    
    public bool Success { get; set; }
}