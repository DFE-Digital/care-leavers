namespace CareLeavers.Web.Translation;

public class NoTranslationService : ITranslationService
{
    public Task<string> Translate(string text, string toLanguage)
    {
        return Task.FromResult(text);
    }

    public Task<TranslationLanguage> GetLanguage(string code)
    {
        return Task.FromResult(new TranslationLanguage()
        {
            Code = code,
            Name = code,
            NativeName = code
        });
    }

    public Task<IEnumerable<TranslationLanguage>> GetLanguages()
    {
        return Task.FromResult(Enumerable.Empty<TranslationLanguage>());
    }
}