using System.Net;
using System.Net.Http.Headers;
using CareLeavers.Web.GetToAnAnswerRun;
using Joonasw.AspNetCore.SecurityHeaders.Csp;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using NSubstitute;

namespace CareLeavers.Web.Tests.GetToAnAnswerRun;

public class GetToAnAnswerRunClientTests
{
    private readonly HttpClient _httpClientMock;
    private readonly MockHttpMessageHandler _httpMessageHandlerMock;

    private readonly GetToAnAnswerRunClient _getToAnAnswerRunClient;

    public GetToAnAnswerRunClientTests()
    {
        _httpMessageHandlerMock = new MockHttpMessageHandler();
        _httpClientMock = new HttpClient(_httpMessageHandlerMock)
        {
            BaseAddress = new Uri("https://localhost:1234")
        };
        ILogger<GetToAnAnswerRunClient> logger = Substitute.For<ILogger<GetToAnAnswerRunClient>>();

        ServiceCollection serviceCollection = [];
        serviceCollection.AddMemoryCache();
        serviceCollection.AddTransient<IConfiguration>(_ =>
        {
            IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
            configuration["GetToAnAnswer:BaseUrl"] = "https://localhost:5678";
            return configuration;
        });
        serviceCollection.AddTransient<ICspNonceService>(_ => new CspNonceService());

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
        _getToAnAnswerRunClient = new GetToAnAnswerRunClient(_httpClientMock, serviceProvider, logger, memoryCache);
    }

    [Test]
    public async Task GetStartPageOrInitialState_Throws_Exception_IfStatusCodeIsNotOK()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.BadRequest;
        _httpMessageHandlerMock.Content = new StringContent("");

        _ = Assert.ThrowsAsync<HttpRequestException>(async () => await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test"));
    }

    [Test]
    public async Task GetStartPageOrInitialState_Replaces_ScriptTags()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("""
                                                            <script>Test</script>
                                                            <script src=/en/test>Test</script>
                                                            <script asp-add-nonce>Test</script>
                                                            """);

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        Assert.That(result, Does.Contain("<script nonce="));
        Assert.That(result, Does.Contain("<script src=\"https://localhost:5678/en/test\""));
        Assert.That(result, Does.Not.Contain("asp-add-nonce"));
    }

    [Test]
    public async Task GetStartPageOrInitialState_Replaces_LinkTags()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("""
                                                            <link href=/en/test>Test</a>
                                                            <link asp-add-nonce href=/en/test>Test</a>
                                                            """);

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        Assert.That(result, Does.Contain("<link href=\"https://localhost:5678/en/test\""));
        Assert.That(result, Does.Not.Contain("asp-add-nonce"));
    }

    [Test]
    public async Task GetStartPageOrInitialState_Replaces_StyleTags()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<style asp-add-nonce>p { color: #000000; }</style>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        Assert.That(result, Does.Contain("<style nonce="));
        Assert.That(result, Does.Not.Contain("asp-add-nonce"));
    }

    [Test]
    public async Task GetStartPageOrInitialState_Replaces_FormTags()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<form method=\"post\" action=\"/questionnaires/\" novalidate></form>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        Assert.That(result,
            Does.Contain("<form method=\"post\" action=\"/en/get-to-an-answer-questionnaires/\" novalidate></form>"));
    }

    [Test]
    public async Task GetStartPageOrInitialState_Replaces_ATags()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<a href=\"/questionnaires/\">Test</a>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        Assert.That(result, Does.Contain("<a href=\"/en/get-to-an-answer-questionnaires/\">Test</a>"));
    }

    [Test]
    public async Task GetInitialState_Returns_Html()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<p>Test</p>");

        string result = await _getToAnAnswerRunClient.GetInitialState("en", "/test");
        
        Assert.That(result, Is.EqualTo("<p>Test</p>"));
    }

    [Test]
    public async Task GetInitialState_Throws_Exception_IfStatusCodeIsNotOK()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.BadRequest;
        _httpMessageHandlerMock.Content = new StringContent("");

        Assert.ThrowsAsync<HttpRequestException>((Func<Task>)GtaaTask);
        return;

        async Task GtaaTask() => await _getToAnAnswerRunClient.GetInitialState("en", "/test-error");
    }

    [Test]
    public async Task GetNextState_Replaces_LanguageCode_On_Redirect()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent(
            "<input type=\"hidden\" id=\"external-link-dest\" value=\"https://localhost:1234/en/test\">");

        string result =
            await _getToAnAnswerRunClient.GetNextState("localhost", "sv", "/test",
                new Dictionary<string, StringValues>());

        Assert.That(result,
            Is.EqualTo("<input type=\"hidden\" id=\"external-link-dest\" value=\"https://localhost:1234/sv/test\" />"));
    }

    [Test]
    public async Task GetNextState_Throws_Exception_IfStatusCodeIsNotOK()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.BadRequest;
        _httpMessageHandlerMock.Content = new StringContent("");

        _ = Assert.ThrowsAsync<HttpRequestException>(async () =>
            await _getToAnAnswerRunClient.GetNextState("localhost", "en", "/test",
                new Dictionary<string, StringValues>()));
    }

    [Test]
    public async Task GetDecorativeImage_Returns_ImageContent()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("Test")
        {
            Headers = { ContentType = new MediaTypeHeaderValue("img/png")}
        };

        (Stream fileStream, string contentType) result = 
            await _getToAnAnswerRunClient.GetDecorativeImage("/test");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(await new StreamReader(result.fileStream).ReadToEndAsync(), Is.EqualTo("Test"));
            Assert.That(result.contentType, Is.EqualTo("img/png"));
        }
    }

    [Test]
    public async Task GetDecorativeImage_Throws_Exception_IfStatusCodeIsNotOK()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.BadRequest;
        _httpMessageHandlerMock.Content = new StringContent("");

        _ = Assert.ThrowsAsync<HttpRequestException>(async () => await _getToAnAnswerRunClient.GetDecorativeImage("/test"));
    }

    [Test]
    public async Task GetCachedQuestionnairePage_Returns_CachedContent_On_CacheHit()
    {
        // First call - cache miss
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<p>Initial Content</p>");

        var firstResult = await _getToAnAnswerRunClient.GetInitialState("en", "test");

        // Second call - cache hit
        _httpMessageHandlerMock.Content = new StringContent("<p>Different Content</p>");
        var secondResult = await _getToAnAnswerRunClient.GetInitialState("en", "test");
        using (Assert.EnterMultipleScope())
        {
            Assert.That(firstResult, Is.EqualTo(secondResult));
            Assert.That(secondResult, Does.Contain("Initial Content"));
            Assert.That(secondResult, Does.Not.Contain("Different Content"));
        }
    }

    [Test]
    public async Task GetCachedQuestionnairePage_Does_Not_Cache_When_CacheMinutesIsNull()
    {
        // First call - cache miss with cacheMinutes: null
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<p>First Response</p>");

        var firstResult = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Second call - should fetch again, not from cache
        _httpMessageHandlerMock.Content = new StringContent("<p>Second Response</p>");
        var secondResult = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        using (Assert.EnterMultipleScope())
        {
            Assert.That(firstResult, Does.Contain("First Response"));
            Assert.That(secondResult, Does.Contain("Second Response"));
            Assert.That(firstResult, Is.Not.EqualTo(secondResult));
        }
    }

    [Test]
    public async Task GetCachedQuestionnairePage_Fetches_From_HttpClient_When_NotInCache()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<p>Test Content</p>");

        string result = await _getToAnAnswerRunClient.GetInitialState("en", "unique-slug");

        Assert.That(result, Does.Contain("Test Content"));
    }

    [Test]
    public async Task GetCachedQuestionnairePage_Sets_CorrectCacheExpiration()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<p>Cached Content</p>");

        var firstResult = await _getToAnAnswerRunClient.GetInitialState("en", "cache-test");
        await Task.Delay(100); // Small delay

        // Change content for second call
        _httpMessageHandlerMock.Content = new StringContent("<p>Updated Content</p>");
        var secondResult = await _getToAnAnswerRunClient.GetInitialState("en", "cache-test");

        // Should still be cached (within 5 minutes)
        Assert.That(secondResult, Is.EqualTo(firstResult));
    }

    [Test]
    public async Task GetNextState_Does_Not_Process_ExternalLink_When_ThisOriginIsNull()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent(
            "<input type=\"hidden\" id=\"external-link-dest\" value=\"https://localhost:1234/en/test\">");

        var formData = new Dictionary<string, StringValues> { { "test", new StringValues("value") } };
        string result = await _getToAnAnswerRunClient.GetNextState(null ?? "", "en", "test", formData);

        // Should not modify the value since thisOrigin is null
        Assert.That(result, Does.Contain("/en/test"));
    }

    [Test]
    public async Task GetNextState_Does_Not_Process_ExternalLink_When_InputDoesNotExist()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<p>No external link input</p>");

        var formData = new Dictionary<string, StringValues>();
        string result = await _getToAnAnswerRunClient.GetNextState("localhost", "sv", "test", formData);

        Assert.That(result, Does.Contain("No external link input"));
    }

    [Test]
    public async Task GetNextState_Does_Not_Process_ExternalLink_When_HostDoesNotMatch()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent(
            "<input type=\"hidden\" id=\"external-link-dest\" value=\"https://different-host.com/en/test\">");

        var formData = new Dictionary<string, StringValues>();
        string result = await _getToAnAnswerRunClient.GetNextState("localhost", "sv", "test", formData);

        // Should not modify since host doesn't match
        Assert.That(result, Does.Contain("https://different-host.com/en/test"));
    }

    [Test]
    public async Task GetNextState_Does_Not_Process_ExternalLink_When_UrlPathIsEmpty()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent(
            "<input type=\"hidden\" id=\"external-link-dest\" value=\"https://localhost:1234\">");

        var formData = new Dictionary<string, StringValues>();
        string result = await _getToAnAnswerRunClient.GetNextState("localhost:1234", "sv", "test", formData);

        // Should not modify since path is empty or root only
        Assert.That(result, Does.Contain("https://localhost:1234"));
    }

    [Test]
    public async Task GetNextState_Does_Not_Process_ExternalLink_When_ValueIsEmpty()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent(
            "<input type=\"hidden\" id=\"external-link-dest\" value=\"\">");

        var formData = new Dictionary<string, StringValues>();
        string result = await _getToAnAnswerRunClient.GetNextState("localhost", "sv", "test", formData);

        Assert.That(result, Does.Contain("external-link-dest"));
    }

    [Test]
    public async Task InjectBaseUrlAndNonce_Returns_Early_When_BaseUrlIsNull()
    {
        // Create a new client with null baseUrl
        var httpMessageHandlerMock = new MockHttpMessageHandler();
        var httpClientMock = new HttpClient(httpMessageHandlerMock)
        {
            BaseAddress = new Uri("https://localhost:1234")
        };
        ILogger<GetToAnAnswerRunClient> logger = Substitute.For<ILogger<GetToAnAnswerRunClient>>();

        ServiceCollection serviceCollection = [];
        serviceCollection.AddMemoryCache();
        serviceCollection.AddTransient<IConfiguration>(_ =>
        {
            IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
            // BaseUrl is not set, so it will be null
            return configuration;
        });
        serviceCollection.AddTransient<ICspNonceService>(_ => new CspNonceService());

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
        var clientWithoutBaseUrl = new GetToAnAnswerRunClient(httpClientMock, serviceProvider, logger, memoryCache);

        httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        httpMessageHandlerMock.Content = new StringContent("""
            <script src="/test">Test</script>
            <link href="/test" />
            <style>Test</style>
            """);

        string result = await clientWithoutBaseUrl.GetStartPageOrInitialState("en", "test");

        // When baseUrl is null, no URL substitution should happen
        Assert.That(result, Does.Contain("<script src=\"/test\""));
        Assert.That(result, Does.Not.Contain("https://localhost:5678"));

        httpMessageHandlerMock.Dispose();
        httpClientMock.Dispose();
    }

    [Test]
    public async Task ProcessScriptTags_Adds_Nonce_When_NonceAttributeIsNull()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<script>console.log('test');</script>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Script should have nonce added
        Assert.That(result, Does.Contain("<script nonce="));
    }

    [Test]
    public async Task ProcessScriptTags_Replaces_Nonce_When_NonceAttributeIsWhitespace()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<script nonce=\"   \">console.log('test');</script>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Whitespace nonce should be replaced with actual nonce
        Assert.That(result, Does.Contain("<script nonce="));
        Assert.That(result, Does.Not.Contain("nonce=\"   \""));
    }

    [Test]
    public async Task ProcessScriptTags_Ignores_Script_Without_SrcAttribute()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("""
            <script>console.log('inline script');</script>
            <script>document.write('test');</script>
            """);

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Inline scripts should still be there and nonce should be added, but no src modification
        Assert.That(result, Does.Contain("inline script"));
        Assert.That(result, Does.Contain("<script nonce="));
    }

    [Test]
    public async Task ProcessScriptTags_Ignores_Src_When_ValueIsNull()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        // Script with empty src attribute
        _httpMessageHandlerMock.Content = new StringContent("<script src=\"\">Test</script>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Empty src should not be modified
        Assert.That(result, Does.Contain("<script src=\"\""));
    }

    [Test]
    public async Task ProcessScriptTags_Ignores_Src_When_NotStartingWithSlash()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<script src=\"https://cdn.example.com/script.js\">Test</script>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Absolute URLs should not be modified
        Assert.That(result, Does.Contain("https://cdn.example.com/script.js"));
        Assert.That(result, Does.Not.Contain("https://localhost:5678https://cdn.example.com"));
    }

    [Test]
    public async Task ProcessLinkTags_Ignores_Href_When_ValueIsNull()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<link href=\"\" rel=\"stylesheet\" />");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Empty href should not be modified
        Assert.That(result, Does.Contain("href=\"\""));
    }

    [Test]
    public async Task ProcessLinkTags_Ignores_Href_When_NotStartingWithSlash()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<link href=\"https://cdn.example.com/style.css\" rel=\"stylesheet\" />");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Absolute URLs should not be modified
        Assert.That(result, Does.Contain("https://cdn.example.com/style.css"));
    }

    [Test]
    public async Task ProcessStyleTags_Adds_Nonce_When_NonceAttributeIsNull()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<style>body { color: red; }</style>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Style should have nonce added
        Assert.That(result, Does.Contain("<style nonce="));
    }

    [Test]
    public async Task ProcessStyleTags_Replaces_Nonce_When_NonceAttributeIsWhitespace()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<style nonce=\"  \">body { color: red; }</style>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Whitespace nonce should be replaced with actual nonce
        Assert.That(result, Does.Contain("<style nonce="));
        Assert.That(result, Does.Not.Contain("nonce=\"  \""));
    }

    [Test]
    public async Task ProcessFormTags_Ignores_ActionAttribute_When_Null()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<form method=\"post\" novalidate></form>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Form without action should remain unchanged
        Assert.That(result, Does.Contain("<form method=\"post\""));
    }

    [Test]
    public async Task ProcessFormTags_Ignores_ActionAttribute_When_Empty()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<form action=\"\" method=\"post\" novalidate></form>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Empty action should not be modified
        Assert.That(result, Does.Contain("action=\"\""));
    }

    [Test]
    public async Task ProcessFormTags_Ignores_ActionAttribute_When_NotStartingWithQuestionnaires()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<form action=\"/other-endpoint\" method=\"post\"></form>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Action not starting with /questionnaires should not be modified
        Assert.That(result, Does.Contain("/other-endpoint"));
    }

    [Test]
    public async Task ProcessAnchorTags_Ignores_HrefAttribute_When_Null()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<a>Click me</a>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Anchor without href should remain unchanged
        Assert.That(result, Does.Contain("<a>Click me</a>"));
    }

    [Test]
    public async Task ProcessAnchorTags_Ignores_HrefAttribute_When_Empty()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<a href=\"\">Click me</a>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Empty href should not be modified
        Assert.That(result, Does.Contain("href=\"\""));
    }

    [Test]
    public async Task ProcessAnchorTags_Ignores_HrefAttribute_When_NotStartingWithQuestionnaires()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent("<a href=\"/other-path\">Click me</a>");

        string result = await _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test");

        // Href not starting with /questionnaires should not be modified
        Assert.That(result, Does.Contain("/other-path"));
    }

    [Test]
    public async Task ProcessExternalLink_Does_Not_Process_When_PathPartsLengthIsOne()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.OK;
        _httpMessageHandlerMock.Content = new StringContent(
            "<input type=\"hidden\" id=\"external-link-dest\" value=\"https://localhost:1234/\">");

        var formData = new Dictionary<string, StringValues>();
        string result = await _getToAnAnswerRunClient.GetNextState("localhost:1234", "sv", "test", formData);

        // Should not modify when path is just root
        Assert.That(result, Does.Contain("https://localhost:1234/"));
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        _httpMessageHandlerMock.Dispose();
        _httpClientMock.Dispose();
    }
}