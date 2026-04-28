using CareLeavers.Web.Contentful;
using CareLeavers.Web.Controllers;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Pdf;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Page = CareLeavers.Web.Models.Content.Page;

namespace CareLeavers.Web.Tests.Controllers;

public class PrintControllerTests
{
    private IContentService _contentService;
    private ITranslationService _translationService;
    private ILogger<PrintController> _logger;
    private IPdfGenerator _pdfGenerator;
    private IUrlHelper _urlHelper;

    private PrintController _printController;

    [SetUp]
    public void Init()
    {
        _contentService = Substitute.For<IContentService>();
        _translationService = Substitute.For<ITranslationService>();
        _logger = Substitute.For<ILogger<PrintController>>();
        _pdfGenerator = Substitute.For<IPdfGenerator>();
        _urlHelper = Substitute.For<IUrlHelper>();

        DefaultHttpContext httpContext = new()
        {
            Request =
            {
                Scheme = "https",
                Host = new HostString("localhost")
            }
        };

        ActionContext actionContext = new()
        {
            HttpContext = httpContext,
            RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
            ActionDescriptor = new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor()
        };
        
        _urlHelper.ActionContext.Returns(actionContext);

        _printController = new PrintController(_contentService, _translationService, Substitute.For<IConfiguration>(), _logger)
        {
            Url = _urlHelper,
            ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
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

        _pdfGenerator.Generate(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<List<string>>())
            .Returns(pdfBytes);

        IActionResult result = await _printController.DownloadPdf(_pdfGenerator, id, "en");
        
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
    public async Task DownloadPdf_WhenUrlIsNull_Returns_BadRequest()
    {
        const string id = "test-id";
        _urlHelper.Action(Arg.Any<UrlActionContext>()).Returns((string?)null);

        IActionResult result = await _printController.DownloadPdf(_pdfGenerator, id, "en");

        Assert.That(result, Is.TypeOf<BadRequestResult>());
    }

    [Test]
    public async Task DownloadPdf_WhenPrintableCollectionIsNull_Returns_NotFound()
    {
        const string id = "test-id";
        _urlHelper.Action(Arg.Any<UrlActionContext>()).Returns("https://localhost:1234");
        _contentService.GetPrintableCollection(id).Returns((PrintableCollection?)null);

        IActionResult result = await _printController.DownloadPdf(_pdfGenerator, id, "en");

        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public async Task DownloadPdf_WhenEmptyPdfGenerated_Returns_NotFound()
    {
        const string id = "test-id";
        PrintableCollection collection = new PrintableCollection { Title = "Test", Identifier = id };
        _contentService.GetPrintableCollection(id).Returns(collection);
        _urlHelper.Action(Arg.Any<UrlActionContext>()).Returns("https://localhost:1234");
        _pdfGenerator.Generate(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<List<string>>())
            .Returns([]);

        IActionResult result = await _printController.DownloadPdf(_pdfGenerator, id, "en");

        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public async Task DownloadPdf_Calls_Generator_With_Correct_Tags()
    {
        const string id = "test-id";
        const string slug1 = "slug-1";
        const string slug2 = "slug-2";
        PrintableCollection collection = new PrintableCollection
        {
            Title = "Test",
            Identifier = id,
            Content =
            [
                new Page { Slug = slug1, Title = "Page 1" },
                new Page { Slug = slug2, Title = "Page 2" },
                new Page { Slug = null, Title = "Page 3" }
            ]
        };
        _contentService.GetPrintableCollection(id).Returns(collection);

        _urlHelper.Action(Arg.Any<UrlActionContext>()).Returns("https://localhost:1234");
        _pdfGenerator.Generate(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<List<string>>())
            .Returns("Test"u8.ToArray());

        await _printController.DownloadPdf(_pdfGenerator, id, "en");

        await _pdfGenerator.Received(1).Generate(
            Arg.Any<string>(),
            id,
            "en",
            Arg.Is<List<string>>(tags =>
                tags.Count == 3 &&
                tags.Contains($"pc-{id}") &&
                tags.Contains(slug1) &&
                tags.Contains(slug2))
        );
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

        _pdfGenerator.Generate(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<List<string>>())
            .Returns(pdfBytes);

        IActionResult result = await _printController.DownloadPdf(_pdfGenerator, id, languageCode);
        
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
        
        _urlHelper.Action(Arg.Any<UrlActionContext>()).Returns("https://localhost:1234");

        _pdfGenerator.Generate(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<List<string>>())
            .ThrowsAsync(new HttpRequestException("Test Exception"));
        
        IActionResult result = await _printController.DownloadPdf(_pdfGenerator, id, languageCode);
        
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
        _printController.Dispose();
    }
}