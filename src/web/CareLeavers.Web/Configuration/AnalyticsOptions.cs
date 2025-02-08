using System.Diagnostics.CodeAnalysis;

namespace CareLeavers.Web.Configuration;

[ExcludeFromCodeCoverage(Justification = "Configuration")]
public class AnalyticsOptions
{
    public static string Name => "Analytics";
    
    public string? GTM { get; init; } = "";

    public string? Clarity { get; init; } = "";
    
}