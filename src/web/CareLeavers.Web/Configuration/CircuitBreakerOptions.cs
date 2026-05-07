using System.Diagnostics.CodeAnalysis;

namespace CareLeavers.Web.Configuration;

[ExcludeFromCodeCoverage(Justification = "Configuration")]
public class CircuitBreakerOptions
{
    public const string Name = "CircuitBreaker";

    public const string AzureTranslationKey = "_AzureTranslation";
    public const string PdfGeneratorKey = "_PdfGenerator";

    public int AzureTranslationLimit { get; set; }
    
    public int PdfGeneratorLimit { get; set; }
}