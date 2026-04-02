using System.Net;
using System.Net.Http.Headers;
using CareLeavers.Web.GetToAnAnswerRun;
using Joonasw.AspNetCore.SecurityHeaders.Csp;
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
        serviceCollection.AddTransient<IConfiguration>(_ =>
        {
            IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
            configuration["GetToAnAnswer:BaseUrl"] = "https://localhost:5678";
            return configuration;
        });
        serviceCollection.AddTransient<ICspNonceService>(_ => new CspNonceService());

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        _getToAnAnswerRunClient = new GetToAnAnswerRunClient(_httpClientMock, serviceProvider, logger);
    }

    [Test]
    public void GetStartPageOrInitialState_Throws_Exception_IfStatusCodeIsNotOK()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.BadRequest;
        _httpMessageHandlerMock.Content = new StringContent("");

        Assert.ThrowsAsync<Exception>(() => _getToAnAnswerRunClient.GetStartPageOrInitialState("en", "test"));
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
    public void GetInitialState_Throws_Exception_IfStatusCodeIsNotOK()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.BadRequest;
        _httpMessageHandlerMock.Content = new StringContent("");
        
        Assert.ThrowsAsync<Exception>(() => _getToAnAnswerRunClient.GetInitialState("en", "/test"));
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
    public void GetNextState_Throws_Exception_IfStatusCodeIsNotOK()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.BadRequest;
        _httpMessageHandlerMock.Content = new StringContent("");

        Assert.ThrowsAsync<Exception>(() =>
            _getToAnAnswerRunClient.GetNextState("localhost", "en", "/test", new Dictionary<string, StringValues>()));
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
    public void GetDecorativeImage_Throws_Exception_IfStatusCodeIsNotOK()
    {
        _httpMessageHandlerMock.StatusCode = HttpStatusCode.BadRequest;
        _httpMessageHandlerMock.Content = new StringContent("");
        
        Assert.ThrowsAsync<Exception>(() => _getToAnAnswerRunClient.GetDecorativeImage("/test"));
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        _httpMessageHandlerMock.Dispose();
        _httpClientMock.Dispose();
    }
}