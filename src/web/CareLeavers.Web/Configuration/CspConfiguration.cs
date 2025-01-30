using System.Diagnostics.CodeAnalysis;

namespace CareLeavers.Web.Configuration;

[ExcludeFromCodeCoverage(Justification = "Configuration")]
public class CspConfiguration
{
    public List<string> AllowScriptUrls { get; init; } = [];

    public List<string> AllowStyleUrls { get; init; } = [];

    public List<string> AllowFontUrls { get; init; } = [];

    public List<string> AllowFrameUrls { get; init; } = [];
}