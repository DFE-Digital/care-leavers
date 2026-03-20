using System.Text;
using Azure;
using Azure.AI.Translation.Document;
using Azure.AI.Translation.Text;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Translation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NSubstitute;
using ZiggyCreatures.Caching.Fusion;
using TranslationLanguage = CareLeavers.Web.Translation.TranslationLanguage;

namespace CareLeavers.Web.Tests.Translation;

public class AzureTranslationServiceTests
{
    private readonly TextTranslationClient _textTranslationClientMock;
    private readonly SingleDocumentTranslationClient _singleDocumentTranslationClientMock;

    private readonly IFusionCache _fusionCache;

    private readonly ITranslationService _azureTranslationService;

    public AzureTranslationServiceTests()
    {
        _textTranslationClientMock = Substitute.For<TextTranslationClient>();
        _singleDocumentTranslationClientMock = Substitute.For<SingleDocumentTranslationClient>();

        MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
        _fusionCache = new FusionCache(Options.Create(new FusionCacheOptions()), memoryCache);

        _azureTranslationService = new AzureTranslationService(
            _textTranslationClientMock,
            _singleDocumentTranslationClientMock,
            _fusionCache,
            Substitute.For<IContentfulConfiguration>());
    }

    [Test]
    public async Task TranslateHtml_WhenLanguageCodeIsEnglish_Returns_Html()
    {
        const string html = "<p>Test</p>";
        const string languageCode = "en";

        await _fusionCache.SetAsync("translation:supported-languages",
            new List<TranslationLanguage> { new() { Code = languageCode } });

        string? result = await _azureTranslationService.TranslateHtml(html, languageCode);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(html));
    }

    [Test]
    public async Task TranslateHtml_WhenHtmlIsLong_Returns_Translated_Html()
    {
        const string languageCode = "sv";
        const string translation = "<p>Test</p>";
        StringBuilder htmlStringBuilder = new("<p>ABC</p>");
        int length = htmlStringBuilder.Length;
        for (int i = 0; i < 50000 / length; i++) htmlStringBuilder.Append("<p>ABC</p>");
        Response<BinaryData> expectedResponse =
            Response.FromValue(BinaryData.FromString(translation), new ResponseMock());

        await _fusionCache.SetAsync("translation:supported-languages",
            new List<TranslationLanguage> { new() { Code = languageCode } });

        _singleDocumentTranslationClientMock
            .TranslateAsync(languageCode, Arg.Any<DocumentTranslateContent>())
            .Returns(expectedResponse);

        string? result = await _azureTranslationService.TranslateHtml(htmlStringBuilder.ToString(), languageCode);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(translation));
    }

    [Test]
    public async Task TranslateHtml_WhenHtmlIsRegularLength_Returns_Translated_Html()
    {
        const string html = "<p>Test</p>";
        const string translation = "<p>Testa</p>";
        const string languageCode = "sv";

        await _fusionCache.SetAsync("translation:supported-languages",
            new List<TranslationLanguage> { new() { Code = languageCode } });

        _textTranslationClientMock.TranslateAsync(Arg.Any<TextTranslationTranslateOptions>())
            .Returns(Response.FromValue<IReadOnlyList<TranslatedTextItem>>(new List<TranslatedTextItem>
                {
                    AITranslationTextModelFactory.TranslatedTextItem(
                        translations:
                        [
                            AITranslationTextModelFactory.TranslationText(targetLanguage: languageCode,
                                text: translation)
                        ])
                },
                new ResponseMock()));

        string? result = await _azureTranslationService.TranslateHtml(html, languageCode);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(translation));
    }

    [Test]
    public async Task TranslateHtml_WhenLanguageCodeIsUnsupported_Returns_English()
    {
        const string html = "<p>Test</p>";

        await _fusionCache.SetAsync("translation:supported-languages", 
            new List<TranslationLanguage> { new() { Code = "ZZ" } });

        string? result = await _azureTranslationService.TranslateHtml(html, "Invalid");

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(html));
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _fusionCache.Dispose();
    }
}