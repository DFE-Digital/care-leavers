namespace CareLeavers.Web.Translation;

public class AzureTranslationOptions
{
    public static string Name = "AzureTranslation";
    
    public string SubscriptionKey { get; set; } = string.Empty;
    
    public string Endpoint { get; set; } = string.Empty;
    
    public string Region { get; set; } = string.Empty;
}