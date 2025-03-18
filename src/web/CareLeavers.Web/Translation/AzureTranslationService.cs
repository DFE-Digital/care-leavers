using Azure;
using Azure.AI.Translation.Text;
using CareLeavers.Web.Caching;
using CareLeavers.Web.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace CareLeavers.Web.Translation;

public class AzureTranslationService : ITranslationService
{
    private readonly TextTranslationClient _azureTranslationClient;
    private readonly IDistributedCache _distributedCache;
    private readonly IContentfulConfiguration _contentfulConfiguration;
    
    public AzureTranslationService(
        IOptions<AzureTranslationOptions> options, 
        IDistributedCache distributedCache, 
        IContentfulConfiguration contentfulConfiguration)
    {
        _azureTranslationClient =
            new TextTranslationClient(
                new AzureKeyCredential(options.Value.AccessKey), 
                new Uri(options.Value.Endpoint),
                options.Value.Region);
        
        _distributedCache = distributedCache;
        _contentfulConfiguration = contentfulConfiguration;
    }

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
        return await _distributedCache.GetOrSetAsync("translation:supported-languages", async () =>
        {
            var config = await _contentfulConfiguration.GetConfiguration();
            
            var languages = await _azureTranslationClient.GetSupportedLanguagesAsync();
            
            return languages.Value.Translation
                .Where(x => !config.ExcludeFromTranslation.Contains(x.Key))
                .Select(l => new TranslationLanguage
                {
                    Code = l.Key,
                    Name = l.Value.Name,
                    NativeName = l.Value.NativeName,
                    Direction = l.Value.Directionality is LanguageDirectionality.LeftToRight ? "ltr" : "rtl"
                }).ToList();
        }) ?? [];
    }
}