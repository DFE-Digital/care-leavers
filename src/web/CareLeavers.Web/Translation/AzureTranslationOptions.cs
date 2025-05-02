namespace CareLeavers.Web.Translation;

public class AzureTranslationOptions
{
    public static string Name = "AzureTranslation";
    
    public string AccessKey { get; set; } = string.Empty;
    
    public string Endpoint { get; set; } = string.Empty;
    
    public string DocumentEndpoint { get; set; } = string.Empty;
    
    public string Region { get; set; } = string.Empty;
}