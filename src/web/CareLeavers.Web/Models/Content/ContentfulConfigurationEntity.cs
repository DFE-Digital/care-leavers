using Contentful.Core.Models;

namespace CareLeavers.Web.Models.Content;

public class ContentfulConfigurationEntity : ContentfulContent
{
    public enum BannerPhase
    {
        Alpha,
        Beta,
        Live
    }
    
    public static string ContentType { get; } = "configuration";

    
    public string ServiceName { get; set; } = string.Empty;

    public BannerPhase Phase { get; set; } = BannerPhase.Beta;
    
    public Page? HomePage { get; set; }

    public List<NavigationElement> Navigation { get; set; } = [];
    
    public Document? Footer { get; set; }

    public string? FeedbackText { get; set; } = string.Empty;

    public string? FeedbackUrl { get; set; } = string.Empty;
    
    public bool TranslationEnabled { get; set; } = false;
    
    public List<string> ExcludeFromTranslation { get; set; } = [];
    
    public Asset? DefaultSeoImage { get; set; }
    
    public Document? TranslationHeader { get; set; }
    
    public Document? ServiceUnavailableContent { get; set; }
    
    public Document? AccessibilityStatement { get; set; }
    
    public Page? PrintableCollectionPage { get; set; }
    
    public string? PrintableCollectionCallToAction { get; set; }
    
    public string? GoogleSiteVerification { get; set; }
    
    public string? BingSiteVerification { get; set; }

    public string DfELogoAltText { get; set; } = string.Empty;
}