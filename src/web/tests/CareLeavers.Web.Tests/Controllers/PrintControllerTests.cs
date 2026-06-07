using CareLeavers.Web.Contentful;
using CareLeavers.Web.Controllers;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using NSubstitute;

namespace CareLeavers.Web.Tests.Controllers;

public class PrintControllerTests
{
    private IContentService _contentService;
    private ITranslationService _translationService;
    private IUrlHelper _urlHelper;
    private IHttpContextAccessor _httpContextAccessor;

    private PrintController _printController;

    [SetUp]
    public void Init()
    {
        _contentService = Substitute.For<IContentService>();
        _translationService = Substitute.For<ITranslationService>();
        _urlHelper = Substitute.For<IUrlHelper>();
        
        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        _httpContextAccessor.HttpContext = new DefaultHttpContext();
        _httpContextAccessor.HttpContext.Session = new MockSession();

        ActionContext actionContext = new ActionContext
        {
            HttpContext = _httpContextAccessor.HttpContext,
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        
        _urlHelper.ActionContext.Returns(actionContext);
        
        _printController = new PrintController(_contentService, _translationService)
        {
            Url = _urlHelper,
            ControllerContext = new ControllerContext
            {
                HttpContext = _httpContextAccessor.HttpContext
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
        await _contentService.DidNotReceive().GetPrintableCollection(Arg.Any<string>());
    }

    [Test]
    public async Task GetPrintableCollection_WhenInvalidLanguageCode_Redirects_ToEnglishView()
    {
        _contentService.GetConfiguration().Returns(new ContentfulConfigurationEntity { TranslationEnabled = true });
        _translationService.GetLanguage(Arg.Any<string>()).Returns(Task.FromResult(new TranslationLanguage { Code = "en" }));

        IActionResult result = await _printController.GetPrintableCollection("test-id", "zz");

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

    [TearDown]
    public void Teardown()
    {
        _printController.Dispose();
    }
}