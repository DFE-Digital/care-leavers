using System.Diagnostics.CodeAnalysis;

namespace CareLeavers.Web.Configuration;

[ExcludeFromCodeCoverage(Justification = "Configuration only")]
public class CachingOptions
{
    public static string Name => "Caching";
    
    public const string Memory = "Memory";
    public const string Redis = "Redis";

    public string Type { get; set; } = string.Empty;
    
    public string? ConnectionString { get; init; }

    public TimeSpan Duration { get; set; } = TimeSpan.FromDays(30);
}