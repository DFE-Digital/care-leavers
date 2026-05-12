namespace CareLeavers.Web.Models.ViewModels;

public sealed class AnalyticsViewModel
{
    public string? GoogleAnalyticsTag { get; init; }
    public string? MicrosoftClarityTag { get; init; }
    
    public bool Consent { get; init; }
}