namespace CareLeavers.Web.Translation;

public interface ITranslationService
{
    Task<string?> TranslateHtml(string text, string toLanguage);
    
    Task<TranslationLanguage> GetLanguage(string code);
    
    Task<IEnumerable<TranslationLanguage>> GetLanguages();
}