using System.Diagnostics.CodeAnalysis;

namespace CareLeavers.Web.Configuration;

[ExcludeFromCodeCoverage(Justification = "Configuration")]
public class ScriptOptions
{
    public static string Name => "Scripts";
    
    public string? GTM { get; init; } = "";

    public string? Clarity { get; init; } = "";
    
    public string? ShareaholicSiteId { get; init; } = "";
    
    public string? ShareaholicAppId { get; init; } = "";
    
    public bool ShowCookieBanner { get; init; }

    public bool AddCssVersion { get; init; } = true;
}