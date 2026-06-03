using System.Diagnostics.CodeAnalysis;

namespace CareLeavers.Web.Configuration;

[ExcludeFromCodeCoverage(Justification = "Configuration")]
public class FairUsageOptions
{
    public const string Name = "FairUsage";

    public const string AzureTranslationKey = "_AzureTranslation";
    public const string AzureTranslationTimeoutKey = "_AzureTranslationTimeout";
    
    public const string PdfGeneratorKey = "_PdfGenerator";

    public int AzureTranslationLimit { get; set; }
    
    public int PdfGeneratorLimit { get; set; }
}