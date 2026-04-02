using CareLeavers.Web.Configuration;
using CareLeavers.Web.Contentful;
using CareLeavers.Web.Controllers;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Tests.Controllers;

public class PrintControllerTests
{
    private IHttpClientFactory _httpClientFactory;
    private IContentService _contentService;
    private ITranslationService _translationService;
    private IOptions<PdfGenerationOptions> _options;
    private IFusionCache _fusionCache;
    private IUrlHelper _urlHelper;

    private PrintController _printController;

    [SetUp]
    public void Init()
    {
        _httpClientFactory = Substitute.For<IHttpClientFactory>();
        _contentService = Substitute.For<IContentService>();
        _translationService = Substitute.For<ITranslationService>();
        _options = Options.Create(new PdfGenerationOptions { ApiKey = "test-key", Sandbox = true });
        _fusionCache = Substitute.For<IFusionCache>();
        _urlHelper = Substitute.For<IUrlHelper>();

        _printController = new PrintController(_httpClientFactory, _contentService, _translationService, _options,
            _fusionCache)
        {
            Url = _urlHelper,
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public async Task GetPrintableCollection_Returns_View()
    {
        const string id = "test-id";
        _contentService.GetConfiguration().Returns(new ContentfulConfigurationEntity());
        PrintableCollection collection = new PrintableCollection { Title = "Test", Identifier = id };
        _contentService.GetPrintableCollection(id).Returns(collection);

        IActionResult result = await _printController.GetPrintableCollection(id, "en");

        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(viewResult.ViewName, Is.EqualTo("Collection"));
            Assert.That(viewResult.Model, Is.EqualTo(collection));
        }
    }

    [Test]
    public async Task GetPrintableCollection_WhenNoLanguageCode_Redirects_ToEnglishView()
    {
        IActionResult result = await _printController.GetPrintableCollection("test-id", null!);

        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        RedirectToActionResult? redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult, Is.Not.Null);
        Assert.That(redirectResult.RouteValues, Is.Not.Null);
        Assert.That(redirectResult.RouteValues["languageCode"], Is.EqualTo("en"));
    }

    [Test]
    public async Task GetPrintableCollection_WhenConfigIsNotFound_Returns_NotFound()
    {
        _contentService.GetConfiguration().Returns((ContentfulConfigurationEntity)null!);

        IActionResult result = await _printController.GetPrintableCollection("test-id", "en");

        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public async Task GetPrintableCollection_WhenInvalidLanguageCode_Redirects_ToEnglishView()
    {
        _contentService.GetConfiguration().Returns(new ContentfulConfigurationEntity { TranslationEnabled = true });
        _translationService.GetLanguages().Returns(new List<TranslationLanguage> { new() { Code = "ZZ" } });

        IActionResult result = await _printController.GetPrintableCollection("test-id", "sv");

        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        RedirectToActionResult? redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult, Is.Not.Null);
        Assert.That(redirectResult.RouteValues, Is.Not.Null);
        Assert.That(redirectResult.RouteValues["languageCode"], Is.EqualTo("en"));
    }

    [Test]
    public async Task GetPrintableCollection_WhenCollectionNotFound_Returns_NotFound()
    {
        const string id = "test-id";
        _contentService.GetConfiguration().Returns(new ContentfulConfigurationEntity());
        _contentService.GetPrintableCollection(id).Returns((PrintableCollection)null!);
        
        IActionResult result = await _printController.GetPrintableCollection(id, "en");
        
        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public async Task DownloadPdf_Returns_File()
    {
        const string id = "test-id";
        PrintableCollection collection = new PrintableCollection { Title = "Test", Identifier = id };
        _contentService.GetPrintableCollection(id).Returns(collection);

        byte[] pdfBytes = "Test"u8.ToArray();

        _urlHelper.Action(Arg.Any<UrlActionContext>()).Returns("https://localhost:1234");

        _fusionCache.GetOrSetAsync(
            Arg.Any<string>(),
            Arg.Any<Func<FusionCacheFactoryExecutionContext<byte[]>, CancellationToken, Task<byte[]>>>(),
            Arg.Any<MaybeValue<byte[]>>(),
            Arg.Any<FusionCacheEntryOptions>(),
            Arg.Any<IEnumerable<string>>(),
            Arg.Any<CancellationToken>()
        ).Returns(pdfBytes);

        IActionResult result = await _printController.DownloadPdf(id, "en");
        
        Assert.That(result, Is.TypeOf<FileContentResult>());
        FileContentResult? fileContent = result as FileContentResult;
        Assert.That(fileContent, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(fileContent.ContentType, Is.EqualTo("application/pdf"));
            Assert.That(fileContent.FileContents, Is.EqualTo(pdfBytes));
        }
    }

    [Test]
    public async Task DownloadPdf_WhenNonEnglish_Appends_CorrectFilename()
    {
        const string id = "test-id";
        const string languageCode = "sv";
        PrintableCollection collection = new PrintableCollection { Title = "Test", Identifier = id };
        _contentService.GetPrintableCollection(id).Returns(collection);

        byte[] pdfBytes = "Test"u8.ToArray();

        _urlHelper.Action(Arg.Any<UrlActionContext>()).Returns("https://localhost:1234");

        _fusionCache.GetOrSetAsync(
            Arg.Any<string>(),
            Arg.Any<Func<FusionCacheFactoryExecutionContext<byte[]>, CancellationToken, Task<byte[]>>>(),
            Arg.Any<MaybeValue<byte[]>>(),
            Arg.Any<FusionCacheEntryOptions>(),
            Arg.Any<IEnumerable<string>>(),
            Arg.Any<CancellationToken>()
        ).Returns(pdfBytes);

        IActionResult result = await _printController.DownloadPdf(id, languageCode);
        
        Assert.That(result, Is.TypeOf<FileContentResult>());
        string? contentDispositionHeader = _printController.Response.Headers.ContentDisposition;
        Assert.That(contentDispositionHeader, Is.Not.Null);
        Assert.That(contentDispositionHeader, Is.EqualTo($"inline;{id}-{languageCode}.pdf"));
    }

    [Test]
    public async Task DownloadPdf_WhenGenerationIsNotPossible_Redirects_ToPrintPage()
    {
        const string id = "test-id";
        const string languageCode = "en";
        PrintableCollection collection = new PrintableCollection { Title = "Test", Identifier = id };
        _contentService.GetPrintableCollection(id).Returns(collection);
        
        _fusionCache.GetOrSetAsync(
            Arg.Any<string>(),
            Arg.Any<Func<FusionCacheFactoryExecutionContext<byte[]>, CancellationToken, Task<byte[]>>>(),
            Arg.Any<MaybeValue<byte[]>>(),
            Arg.Any<FusionCacheEntryOptions>(),
            Arg.Any<IEnumerable<string>>(),
            Arg.Any<CancellationToken>()
        ).ThrowsAsync(new HttpRequestException());
        
        IActionResult result = await _printController.DownloadPdf(id, languageCode);
        
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
        RedirectToActionResult? redirectResult = result as RedirectToActionResult;
        Assert.That(redirectResult, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(redirectResult.RouteValues, Is.Not.Null);
            Assert.That(redirectResult.ActionName, Is.EqualTo("GetPrintableCollection"));
        }

        using (Assert.EnterMultipleScope())
        {
            Assert.That(redirectResult.RouteValues["identifier"], Is.EqualTo(id));
            Assert.That(redirectResult.RouteValues["languageCode"], Is.EqualTo(languageCode));
        }
    }

    [TearDown]
    public void Teardown()
    {
        _fusionCache.Dispose();
        _printController.Dispose();
    }
}