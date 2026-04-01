using System.Text;
using CareLeavers.Web.Configuration;
using CareLeavers.Web.Filters;
using CareLeavers.Web.Models.Content;
using CareLeavers.Web.Translation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Tests.Filters;

public class TranslationAttributeTests
{
    private IFusionCache _fusionCache;
    private ITranslationService _translationService;
    private IContentfulConfiguration _contentfulConfiguration;

    private HttpContext _httpContext;

    private ActionExecutingContext _actionExecutingContext;
    private ResultExecutingContext _resultExecutingContext;

    private TranslationAttribute _translationAttribute;

    [SetUp]
    public void Init()
    {
        _fusionCache = Substitute.For<IFusionCache>();
        _translationService = Substitute.For<ITranslationService>();
        _contentfulConfiguration = Substitute.For<IContentfulConfiguration>();

        ServiceCollection serviceCollection = [];
        serviceCollection.AddSingleton(_fusionCache);
        serviceCollection.AddSingleton(_translationService);
        serviceCollection.AddSingleton(_contentfulConfiguration);
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        _httpContext = new DefaultHttpContext { RequestServices = serviceProvider };

        ActionContext actionContext = new ActionContext(_httpContext, new RouteData(), new ActionDescriptor());

        _actionExecutingContext = new ActionExecutingContext(actionContext, [], new Dictionary<string, object?>(),
            Substitute.For<Controller>());
        _resultExecutingContext =
            new ResultExecutingContext(actionContext, [], new OkResult(), Substitute.For<Controller>());

        _translationAttribute = new TranslationAttribute();
    }

    [Test]
    public async Task OnActionExecutionAsync_WhenTranslation_Disabled_DoesNothing()
    {
        _contentfulConfiguration.GetConfiguration()
            .Returns(new ContentfulConfigurationEntity { TranslationEnabled = false });
        bool nextCalled = false;

        await _translationAttribute.OnActionExecutionAsync(_actionExecutingContext, Next);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(nextCalled, Is.True);
            Assert.That(_actionExecutingContext.Result, Is.Null);
        }

        return;

        Task<ActionExecutedContext> Next()
        {
            nextCalled = true;
            return Task.FromResult(new ActionExecutedContext(_actionExecutingContext, [],
                Substitute.For<Controller>()));
        }
    }

    [Test]
    public async Task OnActionExecutionAsync_WhenLanguageIsEnglish_DoesNothing()
    {
        _contentfulConfiguration.GetConfiguration()
            .Returns(new ContentfulConfigurationEntity { TranslationEnabled = true });
        _actionExecutingContext.RouteData.Values["slug"] = "test-slug";
        _actionExecutingContext.RouteData.Values["languageCode"] = "en";
        bool nextCalled = false;

        await _translationAttribute.OnActionExecutionAsync(_actionExecutingContext, Next);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(nextCalled, Is.True);
            Assert.That(_actionExecutingContext.Result, Is.Null);
        }

        return;

        Task<ActionExecutedContext> Next()
        {
            nextCalled = true;
            return Task.FromResult(new ActionExecutedContext(_actionExecutingContext, [],
                Substitute.For<Controller>()));
        }
    }

    [Test]
    public async Task OnActionExecutionAsync_WhenLanguageIsNotInList_DoesNothing()
    {
        _contentfulConfiguration.GetConfiguration()
            .Returns(new ContentfulConfigurationEntity { TranslationEnabled = true });
        _translationService.GetLanguages().Returns(new List<TranslationLanguage> { new() { Code = "pt" } });
        _actionExecutingContext.RouteData.Values["slug"] = "test-slug";
        _actionExecutingContext.RouteData.Values["languageCode"] = "sv";
        bool nextCalled = false;

        await _translationAttribute.OnActionExecutionAsync(_actionExecutingContext, Next);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(nextCalled, Is.True);
            Assert.That(_httpContext.Response.Body, Is.InstanceOf<Stream>());
            Assert.That(_actionExecutingContext.Result, Is.Null);
        }

        return;

        Task<ActionExecutedContext> Next()
        {
            nextCalled = true;
            return Task.FromResult(new ActionExecutedContext(_actionExecutingContext, [],
                Substitute.For<Controller>()));
        }
    }

    [Test]
    public async Task OnActionExecutionAsync_WhenCache_Hit_SetsResult()
    {
        _contentfulConfiguration.GetConfiguration()
            .Returns(new ContentfulConfigurationEntity { TranslationEnabled = true });
        _translationService.GetLanguages().Returns(new List<TranslationLanguage> { new() { Code = "sv" } });
        _actionExecutingContext.RouteData.Values["slug"] = "test-slug";
        _actionExecutingContext.RouteData.Values["languageCode"] = "sv";
        bool nextCalled = false;

        byte[] cachedHtml = "<p>Testa</p>"u8.ToArray();
        _fusionCache.TryGetAsync<byte[]?>("content:test-slug:language:sv")
            .Returns(MaybeValue<byte[]?>.FromValue(cachedHtml));

        await _translationAttribute.OnActionExecutionAsync(_actionExecutingContext, Next);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(nextCalled, Is.False);
            Assert.That(_actionExecutingContext.Result, Is.TypeOf<ContentResult>());
        }
        ContentResult? contentResult = (ContentResult)_actionExecutingContext.Result;
        Assert.That(contentResult.Content, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(contentResult.Content, Is.EqualTo("<p>Testa</p>"));
            Assert.That(contentResult.ContentType, Is.EqualTo("text/html"));
        }

        return;

        Task<ActionExecutedContext> Next()
        {
            nextCalled = true;
            return Task.FromResult(new ActionExecutedContext(_actionExecutingContext, [],
                Substitute.For<Controller>()));
        }
    }

    [Test]
    public async Task OnActionExecutionAsync_WhenCache_Miss_ReplacesBodyWithStream()
    {
        _contentfulConfiguration.GetConfiguration()
            .Returns(new ContentfulConfigurationEntity { TranslationEnabled = true });
        _translationService.GetLanguages().Returns(new List<TranslationLanguage> { new() { Code = "sv" } });
        _actionExecutingContext.RouteData.Values["slug"] = "test-slug";
        _actionExecutingContext.RouteData.Values["languageCode"] = "sv";
        bool nextCalled = false;

        Stream originalBody = _httpContext.Response.Body;
        _fusionCache.TryGetAsync<byte[]?>("content:test-slug:language:sv").Returns(MaybeValue<byte[]?>.None);

        await _translationAttribute.OnActionExecutionAsync(_actionExecutingContext, Next);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(nextCalled, Is.True);
            Assert.That(_httpContext.Response.Body, Is.Not.SameAs(originalBody));
        }
        Assert.That(_httpContext.Response.Body, Is.TypeOf<MemoryStream>());

        return;

        Task<ActionExecutedContext> Next()
        {
            nextCalled = true;
            return Task.FromResult(new ActionExecutedContext(_actionExecutingContext, [],
                Substitute.For<Controller>()));
        }
    }

    [Test]
    public async Task OnActionExecutionAsync_WhenNoCacheIs_True_DoesNotCheckCache()
    {
        _translationAttribute = new TranslationAttribute { NoCache = true };
        _contentfulConfiguration.GetConfiguration()
            .Returns(new ContentfulConfigurationEntity { TranslationEnabled = true });
        _translationService.GetLanguages().Returns(new List<TranslationLanguage> { new() { Code = "sv" } });
        _actionExecutingContext.RouteData.Values["slug"] = "test-slug";
        _actionExecutingContext.RouteData.Values["languageCode"] = "sv";

        await _translationAttribute.OnActionExecutionAsync(_actionExecutingContext, Next);

        await _fusionCache.DidNotReceiveWithAnyArgs().TryGetAsync<byte[]?>(Arg.Any<string>());

        return;

        Task<ActionExecutedContext> Next() =>
            Task.FromResult(new ActionExecutedContext(_actionExecutingContext, [], Substitute.For<Controller>()));
    }

    [Test]
    public async Task OnActionExecutionAsync_WhenIdentifierIsUsed_SetsCollectionTags()
    {
        _contentfulConfiguration.GetConfiguration()
            .Returns(new ContentfulConfigurationEntity { TranslationEnabled = true });
        _translationService.GetLanguages().Returns(new List<TranslationLanguage> { new() { Code = "sv" } });
        _actionExecutingContext.RouteData.Values["identifier"] = "test-identifier";
        _actionExecutingContext.RouteData.Values["languageCode"] = "sv";

        _fusionCache.TryGetAsync<byte[]?>("collection:test-identifier:language:sv").Returns(MaybeValue<byte[]?>.None);

        await _translationAttribute.OnActionExecutionAsync(_actionExecutingContext, Next);

        await _fusionCache.ReceivedWithAnyArgs().TryGetAsync<byte[]?>("collection:test-identifier:language:sv");

        return;

        Task<ActionExecutedContext> Next() =>
            Task.FromResult(new ActionExecutedContext(_actionExecutingContext, [], Substitute.For<Controller>()));
    }

    [Test]
    public async Task OnResultExecutionAsync_TranslatesContent_And_WritesToStream()
    {
        _contentfulConfiguration.GetConfiguration()
            .Returns(new ContentfulConfigurationEntity { TranslationEnabled = true });
        _translationService.GetLanguages().Returns(new List<TranslationLanguage> { new() { Code = "sv" } });
        _actionExecutingContext.RouteData.Values["slug"] = "test-slug";
        _actionExecutingContext.RouteData.Values["languageCode"] = "sv";

        MemoryStream memoryStream = new MemoryStream();
        _httpContext.Response.Body = memoryStream;

        _fusionCache.TryGetAsync<byte[]?>("content:test-slug:language:sv").Returns(MaybeValue<byte[]?>.None);
        await _translationAttribute.OnActionExecutionAsync(_actionExecutingContext, ActionNext);

        const string originalHtml = "<p>Test</p>";
        const string translatedHtml = "<p>Translated</p>";

        await _httpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(originalHtml));

        _translationService.TranslateHtml(originalHtml, "sv").Returns(translatedHtml);

        await _translationAttribute.OnResultExecutionAsync(_resultExecutingContext, ResultNext);

        memoryStream.Seek(0, SeekOrigin.Begin);
        string result = Encoding.UTF8.GetString(memoryStream.ToArray());
        Assert.That(result, Is.EqualTo(translatedHtml));
        await _fusionCache.Received(1).SetAsync("content:test-slug:language:sv", Arg.Any<byte[]>(),
            Arg.Any<FusionCacheEntryOptions>(), Arg.Any<IEnumerable<string>>());

        return;

        Task<ActionExecutedContext> ActionNext() =>
            Task.FromResult(new ActionExecutedContext(_actionExecutingContext, [], Substitute.For<Controller>()));

        Task<ResultExecutedContext> ResultNext() => Task.FromResult(new ResultExecutedContext(_resultExecutingContext,
            [], _resultExecutingContext.Result, Substitute.For<Controller>()));
    }

    [Test]
    public async Task OnResultExecutionAsync_WhenIdentifierIsUsed_GetsPrintableCollectionFromCache()
    {
        _contentfulConfiguration.GetConfiguration()
            .Returns(new ContentfulConfigurationEntity { TranslationEnabled = true });
        _translationService.GetLanguages().Returns(new List<TranslationLanguage> { new() { Code = "sv" } });
        _actionExecutingContext.RouteData.Values["identifier"] = "test-identifier";
        _actionExecutingContext.RouteData.Values["languageCode"] = "sv";

        PrintableCollection collection = new PrintableCollection
        {
            Title = "Test Collection",
            Identifier = "test-identifier",
            Content = [new Page { Slug = "page-one" }, new Page { Slug = "page-two" }]
        };

        _fusionCache.TryGetAsync<byte[]?>("collection:test-identifier:language:sv").Returns(MaybeValue<byte[]?>.None);
        _fusionCache.TryGetAsync<PrintableCollection>("collection:test-identifier").Returns(MaybeValue<PrintableCollection>.FromValue(collection));
        
        await _translationAttribute.OnActionExecutionAsync(_actionExecutingContext, Next);
        
        await _translationAttribute.OnResultExecutionAsync(_resultExecutingContext, ResultNext);

        await _fusionCache.Received(1).SetAsync(
            "collection:test-identifier:language:sv",
            Arg.Any<byte[]>(),
            Arg.Any<FusionCacheEntryOptions>(),
            Arg.Is<IEnumerable<string>>(tags => tags.Contains("page-one") && tags.Contains("page-two"))
            );

        return;

        Task<ActionExecutedContext> Next() =>
            Task.FromResult(new ActionExecutedContext(_actionExecutingContext, [], Substitute.For<Controller>()));

        Task<ResultExecutedContext> ResultNext() => Task.FromResult(new ResultExecutedContext(_resultExecutingContext,
            [], _resultExecutingContext.Result, Substitute.For<Controller>()));
    }

    [TearDown]
    public void Teardown()
    {
        _fusionCache.Dispose();
    }
}