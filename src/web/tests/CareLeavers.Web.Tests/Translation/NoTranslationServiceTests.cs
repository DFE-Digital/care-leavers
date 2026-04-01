using CareLeavers.Web.Translation;

namespace CareLeavers.Web.Tests.Translation;

public class NoTranslationServiceTests
{
    private NoTranslationService _noTranslationService;

    [SetUp]
    public void Init()
    {
        _noTranslationService = new NoTranslationService();
    }

    [Test]
    public async Task TranslateHtml_Returns_Original_Text()
    {
        const string html = "<p>Test</p>";

        string? result = await _noTranslationService.TranslateHtml(html, "sv");

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(html));
    }

    [Test]
    public async Task GetLanguage_Returns_Language_With_Code_As_Name_And_NativeName()
    {
        const string code = "sv";

        TranslationLanguage result = await _noTranslationService.GetLanguage(code);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.Code, Is.EqualTo(code));
            Assert.That(result.Name, Is.EqualTo(code));
            Assert.That(result.NativeName, Is.EqualTo(code));
        }
    }

    [Test]
    public async Task GetLanguages_Returns_Empty_Enumerable()
    {
        IEnumerable<TranslationLanguage> result = await _noTranslationService.GetLanguages();

        Assert.That(result, Is.Empty);
    }
}
