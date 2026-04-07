using System.Text;
using Azure;
using Azure.AI.Translation.Document;
using Azure.AI.Translation.Text;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Translation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NSubstitute;
using ZiggyCreatures.Caching.Fusion;
using TranslationLanguage = CareLeavers.Web.Translation.TranslationLanguage;

namespace CareLeavers.Web.Tests.Translation;

public class AzureTranslationServiceTests
{
    private TextTranslationClient _textTranslationClientMock;
    private SingleDocumentTranslationClient _singleDocumentTranslationClientMock;
    private FusionCache _fusionCache;
    private IContentfulConfiguration _contentfulConfigurationMock;

    private AzureTranslationService _azureTranslationService;

    [SetUp]
    public void Init()
    {
        _textTranslationClientMock = Substitute.For<TextTranslationClient>();
        _singleDocumentTranslationClientMock = Substitute.For<SingleDocumentTranslationClient>();
        _contentfulConfigurationMock = Substitute.For<IContentfulConfiguration>();

        MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
        _fusionCache = new FusionCache(Options.Create(new FusionCacheOptions()), memoryCache);

        _azureTranslationService = new AzureTranslationService(
            _textTranslationClientMock,
            _singleDocumentTranslationClientMock,
            _fusionCache,
            _contentfulConfigurationMock);
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

    [Test]
    public async Task GetLanguage_Returns_Requested_Language()
    {
        const string languageCode = "sv";
        List<TranslationLanguage> languages =
        [
            new() { Code = "en", Name = "English" },
            new() { Code = languageCode, Name = "Swedish" }
        ];

        await _fusionCache.SetAsync("translation:supported-languages", languages);

        TranslationLanguage result = await _azureTranslationService.GetLanguage(languageCode);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.Code, Is.EqualTo(languageCode));
            Assert.That(result.Name, Is.EqualTo("Swedish"));
        }
    }

    [Test]
    public async Task GetLanguage_Returns_New_Language_If_Not_Found()
    {
        await _fusionCache.SetAsync("translation:supported-languages", new List<TranslationLanguage>());

        TranslationLanguage result = await _azureTranslationService.GetLanguage("ZZ");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Code, Is.EqualTo("en"));
    }

    [Test]
    public async Task GetLanguages_Returns_Languages_From_Azure_Minus_Excluded()
    {
        ContentfulConfigurationEntity config = new ContentfulConfigurationEntity
        {
            ExcludeFromTranslation = ["fr"]
        };
        _contentfulConfigurationMock.GetConfiguration().Returns(config);

        GetSupportedLanguagesResult? azureLanguages = AITranslationTextModelFactory.GetSupportedLanguagesResult(
            translation: new Dictionary<string, Azure.AI.Translation.Text.TranslationLanguage>
            {
                { "en", AITranslationTextModelFactory.TranslationLanguage("English", "English") },
                { "sv", AITranslationTextModelFactory.TranslationLanguage("Swedish", "Svenska") },
                { "fr", AITranslationTextModelFactory.TranslationLanguage("French", "Francais") },
                { "ar", AITranslationTextModelFactory.TranslationLanguage("Arabic", "Arabic", LanguageDirectionality.RightToLeft) }
            });

        _textTranslationClientMock.GetSupportedLanguagesAsync(cancellationToken: Arg.Any<CancellationToken>())
            .Returns(_ => Task.FromResult(Response.FromValue(azureLanguages, new ResponseMock())));

        List<TranslationLanguage> result = (await _azureTranslationService.GetLanguages()).ToList();

        Assert.That(result, Has.Count.EqualTo(3));
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.Any(l => l.Code == "fr"), Is.False);
            Assert.That(result.First(l => l.Code == "ar").Direction, Is.EqualTo("rtl"));
            Assert.That(result.First(l => l.Code == "sv").Direction, Is.EqualTo("ltr"));
            Assert.That(result.First(l => l.Code == "sv").NativeName, Is.EqualTo("Svenska"));
        }
    }

    [TearDown]
    public void Teardown()
    {
        _fusionCache.Dispose();
    }
}