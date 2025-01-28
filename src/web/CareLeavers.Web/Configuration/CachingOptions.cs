namespace CareLeavers.Web.Configuration;

public class CachingOptions
{
    public static string Name => "Caching";
    
    public const string Memory = "Memory";
    public const string Redis = "Redis";

    public string Type { get; set; } = string.Empty;
    
    public string? ConnectionString { get; set; }
}