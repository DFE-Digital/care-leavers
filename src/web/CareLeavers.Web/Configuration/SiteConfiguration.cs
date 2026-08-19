using System.Diagnostics.CodeAnalysis;

namespace CareLeavers.Web.Configuration;

[ExcludeFromCodeCoverage(Justification = "Configuration")]
public static class SiteConfiguration
{
    public static bool Rebrand { get; set; }

    public static string? SecurityTxtUrl { get; set; }
}