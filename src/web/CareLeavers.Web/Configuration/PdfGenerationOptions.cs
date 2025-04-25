using System.Diagnostics.CodeAnalysis;

namespace CareLeavers.Web.Configuration;

[ExcludeFromCodeCoverage(Justification = "Configuration only")]
public class PdfGenerationOptions
{
    public static string Name => "PdfGeneration";

    public string ApiKey { get; set; } = string.Empty;

    public bool Sandbox { get; set; } = false;

}