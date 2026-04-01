using CareLeavers.Web.Contentful;
using CareLeavers.Web.Controllers;
using CareLeavers.Web.GetToAnAnswerRun;
using CareLeavers.Web.Models.Content;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using NSubstitute;

namespace CareLeavers.Web.Tests.Controllers;

public class GetToAnAnswerControllerTests
{
    private IContentService _contentService;
    private IGetToAnAnswerRunClient _getToAnAnswerRunClient;
    private ILogger<ContentfulController> _logger;

    private GetToAnAnswerController _getToAnAnswerController;

    [SetUp]
    public void Init()
    {
        _contentService = Substitute.For<IContentService>();
        _getToAnAnswerRunClient = Substitute.For<IGetToAnAnswerRunClient>();
        _logger = Substitute.For<ILogger<ContentfulController>>();

        _getToAnAnswerController = new GetToAnAnswerController(_contentService, _getToAnAnswerRunClient, _logger);
    }

    [Test]
    public async Task EmbeddedGetToAnAnswer_Returns_ViewWithPage()
    {
        const string slug = "gtaa-slug";
        GetToAnAnswer hydratedGtaa = new() { questionnaireSlug = slug };
        _contentService.Hydrate(Arg.Any<GetToAnAnswer>()).Returns(hydratedGtaa);

        IActionResult result = await _getToAnAnswerController.EmbeddedGetToAnAnswer(slug);

        Assert.That(result, Is.TypeOf<ViewResult>());
        ViewResult? viewResult = result as ViewResult;
        Assert.That(viewResult, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(viewResult.ViewName, Is.EqualTo("EmbeddedGetToAnAnswer"));
            Assert.That(viewResult.Model, Is.TypeOf<Page>());
        }
        Page? page = viewResult.Model as Page;
        Assert.That(page, Is.Not.Null);
        Assert.That(page.MainContent, Is.Not.Null);
        Assert.That(page.MainContent.Content, Has.Count.EqualTo(1));
        Assert.That(page.MainContent.Content[0], Is.EqualTo(hydratedGtaa));
    }

    [Test]
    public async Task GetStartPageOrInitialState_Returns_Content()
    {
        const string languageCode = "en";
        const string slug = "gtaa-slug";
        const string html = "<p>Test</p>";
        _getToAnAnswerRunClient.GetStartPageOrInitialState(languageCode, slug).Returns(html);

        IActionResult result = await _getToAnAnswerController.GetStartPageOrInitialState(languageCode, slug);

        Assert.That(result, Is.TypeOf<ContentResult>());
        ContentResult? contentResult = result as ContentResult;
        Assert.That(contentResult, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(contentResult.Content, Is.EqualTo(html));
            Assert.That(contentResult.ContentType, Is.EqualTo("text/html"));
        }
    }

    [Test]
    public async Task GetStartPageOrInitialState_WhenStartStateIsNotDetermined_Returns_NotFound()
    {
        Exception exception = new Exception("Error");
        _getToAnAnswerRunClient.GetStartPageOrInitialState(Arg.Any<string>(), Arg.Any<string>())
            .Returns(Task.FromException<string>(exception));

        IActionResult result = await _getToAnAnswerController.GetStartPageOrInitialState("en", "gtaa-slug");

        Assert.That(result, Is.TypeOf<NotFoundResult>());
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Any<object>(),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Test]
    public async Task GetInitialState_Returns_Content()
    {
        const string languageCode = "en";
        const string slug = "gtaa-slug";
        const string html = "<p>Test</p>";
        _getToAnAnswerRunClient.GetInitialState(languageCode, slug).Returns(html);

        IActionResult result = await _getToAnAnswerController.GetInitialState(languageCode, slug);

        Assert.That(result, Is.TypeOf<ContentResult>());
        ContentResult? contentResult = result as ContentResult;
        Assert.That(contentResult, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(contentResult.Content, Is.EqualTo(html));
            Assert.That(contentResult.ContentType, Is.EqualTo("text/html"));
        }
    }

    [Test]
    public async Task GetInitialState_WhenStartStateIsNotDetermined_Returns_NotFound()
    {
        Exception exception = new Exception("Error");
        _getToAnAnswerRunClient.GetInitialState(Arg.Any<string>(), Arg.Any<string>())
            .Returns(Task.FromException<string>(exception));

        IActionResult result = await _getToAnAnswerController.GetInitialState("en", "gtaa-slug");

        Assert.That(result, Is.TypeOf<NotFoundResult>());
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Any<object>(),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Test]
    public async Task GetNextState_Returns_Content()
    {
        const string languageCode = "en";
        const string slug = "gtaa-slug";
        const string html = "<p>Test</p>";
        const string host = "test-host";

        HttpContext httpContext = new DefaultHttpContext();
        httpContext.Request.Host = new HostString(host);
        Dictionary<string, StringValues> form = new Dictionary<string, StringValues> { { "field-1", "test" } };
        httpContext.Request.Form = new FormCollection(form);

        _getToAnAnswerController.ControllerContext = new ControllerContext { HttpContext = httpContext };
        _getToAnAnswerRunClient.GetNextState(host, languageCode, slug, Arg.Any<Dictionary<string, StringValues>>())
            .Returns(html);

        IActionResult result = await _getToAnAnswerController.GetNextState(languageCode, slug);

        Assert.That(result, Is.TypeOf<ContentResult>());
        ContentResult? contentResult = result as ContentResult;
        Assert.That(contentResult, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(contentResult.Content, Is.EqualTo(html));
            Assert.That(contentResult.ContentType, Is.EqualTo("text/html"));
        }
    }

    [Test]
    public async Task GetNextState_WhenNextStateIsNotDetermined_Returns_NotFound()
    {
        HttpContext httpContext = new DefaultHttpContext();
        httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>());
        _getToAnAnswerController.ControllerContext = new ControllerContext { HttpContext = httpContext };

        Exception exception = new Exception("Error");
        _getToAnAnswerRunClient
            .GetNextState(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<Dictionary<string, StringValues>>()).Returns(Task.FromException<string>(exception));

        IActionResult result = await _getToAnAnswerController.GetNextState("en", "gtaa-slug");
        Assert.That(result, Is.TypeOf<NotFoundResult>());
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Any<object>(),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [Test]
    public async Task GetDecorativeImage_Returns_Content()
    {
        const string slug = "gtaa-slug";
        MemoryStream stream = new MemoryStream();
        const string contentType = "image/png";
        _getToAnAnswerRunClient.GetDecorativeImage(slug).Returns((stream, contentType));

        IActionResult result = await _getToAnAnswerController.GetDecorativeImage("en", slug);

        Assert.That(result, Is.TypeOf<FileStreamResult>());
        FileStreamResult? streamResult = result as FileStreamResult;
        Assert.That(streamResult, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(streamResult.FileStream, Is.EqualTo(stream));
            Assert.That(streamResult.ContentType, Is.EqualTo(contentType));
        }
    }

    [Test]
    public async Task GetDecorativeImage_WhenNextStateIsNotDetermined_Returns_NotFound()
    {
        Exception exception = new Exception("Error");
        _getToAnAnswerRunClient.GetDecorativeImage(Arg.Any<string>())
            .Returns(Task.FromException<(Stream fileStream, string contentType)>(exception));

        IActionResult result = await _getToAnAnswerController.GetDecorativeImage("en", "gtaa-slug");

        Assert.That(result, Is.TypeOf<NotFoundResult>());
        _logger.Received(1).Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Any<object>(),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }

    [TearDown]
    public void Teardown()
    {
        _getToAnAnswerController.Dispose();
    }
}