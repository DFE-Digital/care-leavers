using System.Text;
using CareLeavers.Web.CircuitBreaker.FairUsage;
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
using Microsoft.Extensions.Options;
using NSubstitute;
using ZiggyCreatures.Caching.Fusion;

namespace CareLeavers.Web.Tests.Filters;

public class TranslationFilterTests
{
    private IFusionCache _fusionCache;
    private ITranslationService _translationService;

    private IHttpContextAccessor _httpContextAccessor;
    private HttpContext _httpContext;
    private IOptions<FairUsageOptions> _fairUsageOptions;
    private FairUsageService _fairUsageService;

    private ResultExecutingContext _resultExecutingContext;
    private TranslationFilter _translationFilter;

    [SetUp]
    public void Init()
    {
        _fusionCache = Substitute.For<IFusionCache>();
        _translationService = Substitute.For<ITranslationService>();

        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        _httpContextAccessor.HttpContext = new DefaultHttpContext();
        _httpContextAccessor.HttpContext.Session = new MockSession();
        _fairUsageOptions = Options.Create(new FairUsageOptions { AzureTranslationLimit = int.MaxValue });
        _fairUsageService = new FairUsageService(_httpContextAccessor, _fairUsageOptions);
        
        ServiceCollection serviceCollection = [];
        serviceCollection.AddSingleton(_fusionCache);
        serviceCollection.AddSingleton(_translationService);
        serviceCollection.AddSingleton(_fairUsageService);
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        _httpContextAccessor.HttpContext.RequestServices = serviceProvider;
        _httpContext = _httpContextAccessor.HttpContext;

        ActionContext actionContext = new ActionContext(_httpContext, new RouteData(), new ActionDescriptor());
        _resultExecutingContext =
            new ResultExecutingContext(actionContext, [], new OkResult(), Substitute.For<Controller>());

        _translationFilter = new TranslationFilter(_fusionCache, _translationService, _fairUsageService);
    }

    [Test]
    public async Task OnResultExecutionAsync_When_Language_Is_English_ShouldNotTranslate()
    {
        _resultExecutingContext.RouteData.Values["languageCode"] = "en";

        await _translationFilter.OnResultExecutionAsync(_resultExecutingContext, Next);
        
        Assert.That(_resultExecutingContext.Result, Is.TypeOf<OkResult>());
        await _translationService.DidNotReceiveWithAnyArgs().TranslateHtml(Arg.Any<string>(), Arg.Any<string>());
        
        return;
        Task<ResultExecutedContext> Next() => Task.FromResult(new ResultExecutedContext(_resultExecutingContext,
            [], _resultExecutingContext.Result, Substitute.For<Controller>()));
    }
    
    [Test]
    public async Task OnResultExecutionAsync_When_Language_Is_Invalid_ShouldNotTranslate()
    {
        _resultExecutingContext.RouteData.Values["languageCode"] = "INVALID";
        _translationService.GetLanguage(Arg.Any<string>()).Returns(new TranslationLanguage { Code = "en" });

        await _translationFilter.OnResultExecutionAsync(_resultExecutingContext, Next);
        
        Assert.That(_resultExecutingContext.Result, Is.TypeOf<OkResult>());
        await _translationService.DidNotReceiveWithAnyArgs().TranslateHtml(Arg.Any<string>(), Arg.Any<string>());
        
        return;
        Task<ResultExecutedContext> Next() => Task.FromResult(new ResultExecutedContext(_resultExecutingContext,
            [], _resultExecutingContext.Result, Substitute.For<Controller>()));
    }
    
    [Test]
    public async Task OnResultExecutionAsync_When_Language_Is_Null_ShouldNotTranslate()
    {
        _resultExecutingContext.RouteData.Values["languageCode"] = null;

        await _translationFilter.OnResultExecutionAsync(_resultExecutingContext, Next);
        
        Assert.That(_resultExecutingContext.Result, Is.TypeOf<OkResult>());
        await _translationService.DidNotReceiveWithAnyArgs().TranslateHtml(Arg.Any<string>(), Arg.Any<string>());
        
        return;
        Task<ResultExecutedContext> Next() => Task.FromResult(new ResultExecutedContext(_resultExecutingContext,
            [], _resultExecutingContext.Result, Substitute.For<Controller>()));
    }

    [Test]
    public async Task OnResultExecutionAsync_When_FairUsage_Is_Exceeded_ShouldNotTranslate()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _resultExecutingContext.RouteData.Values["languageCode"] = "sv";
        
        _translationService.GetLanguage(Arg.Any<string>()).Returns(new TranslationLanguage { Code = "sv" });
        _fairUsageOptions.Value.AzureTranslationLimit = 0;
        
        await _translationFilter.OnResultExecutionAsync(_resultExecutingContext, Next);
        
        Assert.That(_resultExecutingContext.Result, Is.TypeOf<RedirectToActionResult>());
        await _translationService.DidNotReceiveWithAnyArgs().TranslateHtml(Arg.Any<string>(), Arg.Any<string>());
        
        return;
        Task<ResultExecutedContext> Next() => Task.FromResult(new ResultExecutedContext(_resultExecutingContext,
            [], _resultExecutingContext.Result, Substitute.For<Controller>()));
    }
    
    [Test]
    public async Task OnResultExecutionAsync_When_NoCache_Translates_Without_Caching()
    {
        _translationFilter = new TranslationFilter(_fusionCache, _translationService, _fairUsageService, true);
        
        const string originalHtml = "<p>Hello</p>";
        const string translatedHtml = "<p>Hej</p>";
        
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _resultExecutingContext.RouteData.Values["languageCode"] = "sv";
        
        _resultExecutingContext.RouteData.Values["slug"] = "test-page";
        _translationService.GetLanguage(Arg.Any<string>()).Returns(new TranslationLanguage { Code = "sv" });
        _translationService.TranslateHtml(originalHtml, "sv").Returns(translatedHtml);
        
        using MemoryStream responseStream = new();
        _httpContext.Response.Body = responseStream;
        
        await _translationFilter.OnResultExecutionAsync(_resultExecutingContext, Next);

        responseStream.Position = 0;
        using StreamReader reader = new(responseStream);
        string result = await reader.ReadToEndAsync();
        
        Assert.That(result, Is.EqualTo(translatedHtml));
        
        return;
        async Task<ResultExecutedContext> Next()
        {
            await _httpContext.Response.WriteAsync(originalHtml, Encoding.UTF8);
            return new ResultExecutedContext(_resultExecutingContext, [], _resultExecutingContext.Result, Substitute.For<Controller>());
        }
    }

    [Test]
    public async Task OnResultExecutionAsync_When_Cache_And_Content_Is_Page_Translates_With_Cache()
    {
        const string originalHtml = "<p>Hello</p>";
        const string translatedHtml = "<p>Hej</p>";
        
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _resultExecutingContext.RouteData.Values["languageCode"] = "sv";
        _resultExecutingContext.RouteData.Values["slug"] = "test-page";
        
        _translationService.GetLanguage(Arg.Any<string>()).Returns(new TranslationLanguage { Code = "sv" });
        _translationService.TranslateHtml(originalHtml, "sv").Returns(translatedHtml);
        
        _fusionCache.GetOrSetAsync(
            Arg.Any<string>(),
            Arg.Any<Func<FusionCacheFactoryExecutionContext<string?>, CancellationToken, Task<string?>>>(),
            Arg.Any<MaybeValue<string?>>(),
            Arg.Any<FusionCacheEntryOptions>(),
            Arg.Any<IEnumerable<string>>(),
            Arg.Any<CancellationToken>()
        ).Returns(translatedHtml);
        
        using MemoryStream responseStream = new();
        _httpContext.Response.Body = responseStream;
        
        await _translationFilter.OnResultExecutionAsync(_resultExecutingContext, Next);

        responseStream.Position = 0;
        using StreamReader reader = new(responseStream);
        string result = await reader.ReadToEndAsync();
        
        Assert.That(result, Is.EqualTo(translatedHtml));
        
        return;
        async Task<ResultExecutedContext> Next()
        {
            await _httpContext.Response.WriteAsync(originalHtml, Encoding.UTF8);
            return new ResultExecutedContext(_resultExecutingContext, [], _resultExecutingContext.Result, Substitute.For<Controller>());
        }
    }
    
    [Test]
    public async Task OnResultExecutionAsync_When_Cache_And_Content_Is_Collection_Translates_With_Cache()
    {
        const string originalHtml = "<p>Hello</p>";
        const string translatedHtml = "<p>Hej</p>";
        const string collectionId = "test-collection";
        
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _resultExecutingContext.RouteData.Values["languageCode"] = "sv";
        _resultExecutingContext.RouteData.Values["identifier"] = collectionId;

        _translationService.GetLanguage(Arg.Any<string>()).Returns(new TranslationLanguage { Code = "sv" });
        _translationService.TranslateHtml(originalHtml, "sv").Returns(translatedHtml);
        
        PrintableCollection collection = new PrintableCollection
        {
            Title = "Test Collection",
            Identifier = "test-identifier",
            Content = [new Page { Slug = "page-one" }, new Page { Slug = "page-two" }]
        };
        _fusionCache.TryGetAsync<PrintableCollection>($"collection:{collectionId}")
            .Returns(MaybeValue<PrintableCollection>.FromValue(collection));
        
        _fusionCache.GetOrSetAsync(
            Arg.Any<string>(),
            Arg.Any<Func<FusionCacheFactoryExecutionContext<string?>, CancellationToken, Task<string?>>>(),
            Arg.Any<MaybeValue<string?>>(),
            Arg.Any<FusionCacheEntryOptions>(),
            Arg.Any<IEnumerable<string>>(),
            Arg.Any<CancellationToken>()
        ).Returns(translatedHtml);
        
        using MemoryStream responseStream = new();
        _httpContext.Response.Body = responseStream;
        
        await _translationFilter.OnResultExecutionAsync(_resultExecutingContext, Next);

        responseStream.Position = 0;
        using StreamReader reader = new(responseStream);
        string result = await reader.ReadToEndAsync();
        
        Assert.That(result, Is.EqualTo(translatedHtml));
        
        return;
        async Task<ResultExecutedContext> Next()
        {
            await _httpContext.Response.WriteAsync(originalHtml, Encoding.UTF8);
            return new ResultExecutedContext(_resultExecutingContext,
                [], _resultExecutingContext.Result, Substitute.For<Controller>());
        }
    }

    [TearDown]
    public void Teardown()
    {
        _fusionCache.Dispose();
    }
}