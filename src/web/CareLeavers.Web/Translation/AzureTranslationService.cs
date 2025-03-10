using Azure;
using Azure.AI.Translation.Text;
using CareLeavers.Web.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace CareLeavers.Web.Translation;

public class AzureTranslationService : ITranslationService
{
    private readonly TextTranslationClient _translationClient;
    private readonly IDistributedCache _distributedCache;
    
    public AzureTranslationService(IOptions<AzureTranslationOptions> options, IDistributedCache distributedCache)
    {
        _translationClient =
            new TextTranslationClient(new AzureKeyCredential(options.Value.SubscriptionKey), new Uri(options.Value.Endpoint));
        
        _distributedCache = distributedCache;
    }

    public Task<string> Translate(string text, string toLanguage)
    {
        return Task.FromResult(text);
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
            var languages = await _translationClient.GetSupportedLanguagesAsync();

            return languages.Value.Translation.Select(l => new TranslationLanguage
            {
                Code = l.Key,
                Name = l.Value.Name,
                NativeName = l.Value.NativeName
            });
            
        }) ?? [];
    }
}