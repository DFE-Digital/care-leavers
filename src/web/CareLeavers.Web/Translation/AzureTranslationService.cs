using System.Text;
using Azure;
using Azure.AI.Translation.Document;
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

    private readonly SingleDocumentTranslationClient _documentTranslationClient = new(
        new Uri(options.Value.DocumentEndpoint),
        new AzureKeyCredential(options.Value.AccessKey)
    );
    
    public async Task<string?> TranslateHtml(string html, string toLanguage)
    {
        var language = await GetLanguage(toLanguage);

        if (language.Code == "en")
        {
            return html;
        }

        if (html.Length >= 50000)
        {
            return await TranslateDocument(html, language.Code);
        }
        
        var translateOptions = new TextTranslationTranslateOptions(toLanguage, html)
        {
            TextType = TextType.Html
        };
        
        var response = await _azureTranslationClient.TranslateAsync(translateOptions);

        return response.Value.FirstOrDefault()?.Translations.FirstOrDefault()?.Text;
    }

    private async Task<string?> TranslateDocument(string html, string language)
    {
        using var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        await writer.WriteAsync(html);
        await writer.FlushAsync();
        stream.Position = 0;

        var sourceDocument = new MultipartFormFileData("source.html", stream, "text/html");
        var content = new DocumentTranslateContent(sourceDocument);

        var response = await _documentTranslationClient.TranslateAsync(language, content).ConfigureAwait(false);

        var responseString = Encoding.UTF8.GetString(response.Value.ToArray());
        return responseString;
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