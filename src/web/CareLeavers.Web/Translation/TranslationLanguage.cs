namespace CareLeavers.Web.Translation;

public class TranslationLanguage
{
    public static TranslationLanguage Default { get; } = new()
    {
        Code = "en",
        Name = "English",
        NativeName = "English"
    };
    
    public string Code { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
    
    public string NativeName { get; set; } = string.Empty;
}