using Azure;
using Azure.AI.Translation.Text;
using CareLeavers.Web.Configuration;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Translation;

public class AzureTranslationService(
    IOptions<AzureTranslationOptions> options,
    IFusionCache fusionCache,
    IContentfulConfiguration contentfulConfiguration)
    : ITranslationService
{
    private readonly TextTranslationClient _azureTranslationClient = new(
        new AzureKeyCredential(options.Value.AccessKey), 
        new Uri(options.Value.Endpoint),
        options.Value.Region);

    public async Task<string?> TranslateHtml(string html, string toLanguage)
    {
        var language = await GetLanguage(toLanguage);

        if (language.Code == "en")
        {
            return html;
        }
        
        var translateOptions = new TextTranslationTranslateOptions(toLanguage, html)
        {
            TextType = TextType.Html
        };
        
        var response = await _azureTranslationClient.TranslateAsync(translateOptions);

        return response.Value.FirstOrDefault()?.Translations.FirstOrDefault()?.Text;
    }

    public async Task<TranslationLanguage> GetLanguage(string code)
    {
        var languages = await GetLanguages();
        
        return languages.FirstOrDefault(l => l.Code == code) ?? new TranslationLanguage();
    }

    public async Task<IEnumerable<TranslationLanguage>> GetLanguages()
    {
        return await fusionCache.GetOrSetAsync("translation:supported-languages", async token =>
        {
            var config = await contentfulConfiguration.GetConfiguration();
            
            var languages = await _azureTranslationClient.GetSupportedLanguagesAsync(cancellationToken: token);
            
            return languages.Value.Translation
                .Where(x => !config.ExcludeFromTranslation.Contains(x.Key))
                .Select(l => new TranslationLanguage
                {
                    Code = l.Key,
                    Name = l.Value.Name,
                    NativeName = l.Value.NativeName,
                    Direction = l.Value.Directionality is LanguageDirectionality.LeftToRight ? "ltr" : "rtl"
                })
                .OrderBy(l => l.Name)
                .ToList();
        });
    }
}