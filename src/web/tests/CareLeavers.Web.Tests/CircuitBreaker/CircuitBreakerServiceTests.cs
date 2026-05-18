using System.Text.Json;
using CareLeavers.Web.CircuitBreaker;
using CareLeavers.Web.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace CareLeavers.Web.Tests.CircuitBreaker;

public class CircuitBreakerServiceTests
{
    private IHttpContextAccessor _httpContextAccessor;
    private HttpContext _httpContext;
    private IOptions<CircuitBreakerOptions> _circuitBreakerOptions;
    private CircuitBreakerService _circuitBreakerService;

    [SetUp]
    public void Init()
    {
        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        _httpContext = new DefaultHttpContext();
        _httpContext.Session = new MockSession();
        _httpContextAccessor.HttpContext = _httpContext;

        _circuitBreakerOptions = Options.Create(new CircuitBreakerOptions
        {
            AzureTranslationLimit = 2,
            PdfGeneratorLimit = 2
        });

        _circuitBreakerService = new CircuitBreakerService(_httpContextAccessor, _circuitBreakerOptions);
    }

    [Test]
    public void ShouldBreakCircuit_When_HttpContextIsNull_Throws_InvalidOperationException()
    {
        _httpContextAccessor.HttpContext = null;

        Assert.That((Func<bool>)CircuitBreaker,
            Throws.TypeOf<InvalidOperationException>().With.Message.EqualTo("HttpContext is NULL"));
        return;

        bool CircuitBreaker() => _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);
    }

    [Test]
    public void ShouldBreakCircuit_When_CircuitBreakerType_IsNull_Throws_ArgumentOutOfRangeException()
    {
        Assert.That((Func<bool>)CircuitBreaker,
            Throws.TypeOf<ArgumentOutOfRangeException>().With.Message
                .EqualTo("Specified argument was out of the range of valid values. (Parameter 'circuit')"));
        return;

        bool CircuitBreaker() => _circuitBreakerService.ShouldBreakCircuit((CircuitBreakerType)2);
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_LanguageCode_IsNull_Returns_False()
    {
        _httpContext.Request.RouteValues["languageCode"] = null;

        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);

        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_LanguageCode_IsEnglish_Returns_False()
    {
        _httpContext.Request.RouteValues["languageCode"] = "en";

        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);

        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_JsonIsNull_Returns_False()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";

        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(_httpContext.Session.GetString(CircuitBreakerOptions.AzureTranslationKey),
                Is.EqualTo(JsonSerializer.Serialize(new List<string> { "sv" })));
        }
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_JsonValueIsNull_Returns_False()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _httpContext.Session.SetString(CircuitBreakerOptions.AzureTranslationKey, "null");

        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(_httpContext.Session.GetString(CircuitBreakerOptions.AzureTranslationKey),
                Is.EqualTo(JsonSerializer.Serialize(new List<string> { "sv" })));
        }
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_LanguagePreviouslyTranslated_Returns_False()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _httpContext.Session.SetString(CircuitBreakerOptions.AzureTranslationKey,
            JsonSerializer.Serialize(new List<string> { "sv" }));

        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);

        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_AtLimit_But_LanguagePreviouslyTranslated_Returns_False()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _httpContext.Session.SetString(CircuitBreakerOptions.AzureTranslationKey,
            JsonSerializer.Serialize(new List<string> { "sv", "fr" }));

        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);

        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_LimitIsReached_Returns_True()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _httpContext.Session.SetString(CircuitBreakerOptions.AzureTranslationKey,
            JsonSerializer.Serialize(new List<string> { "es", "fr" }));

        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);

        Assert.That(result, Is.True);
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_LimitIsNotReached_Returns_False_And_UpdatesSession()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _httpContext.Session.SetString(CircuitBreakerOptions.AzureTranslationKey,
            JsonSerializer.Serialize(new List<string> { "es" }));

        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(_httpContext.Session.GetString(CircuitBreakerOptions.AzureTranslationKey),
                Is.EqualTo(JsonSerializer.Serialize(new List<string> { "es", "sv" })));
        }
    }

    [Test]
    public void ShouldBreakCircuit_PdfGenerator_When_FirstUsage_Returns_False_And_UpdatesSession()
    {
        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.PdfGenerator);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(_httpContext.Session.GetInt32(CircuitBreakerOptions.PdfGeneratorKey), Is.EqualTo(1));
        }
    }

    [Test]
    public void ShouldBreakCircuit_PdfGenerator_When_BelowLimit_Returns_False_And_UpdatesSession()
    {
        _httpContext.Session.SetInt32(CircuitBreakerOptions.PdfGeneratorKey, 1);

        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.PdfGenerator);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(_httpContext.Session.GetInt32(CircuitBreakerOptions.PdfGeneratorKey), Is.EqualTo(2));
        }
    }

    [Test]
    public void ShouldBreakCircuit_PdfGenerator_When_LimitIsReached_Returns_True()
    {
        _httpContext.Session.SetInt32(CircuitBreakerOptions.PdfGeneratorKey, 2);

        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.PdfGenerator);

        Assert.That(result, Is.True);
    }
}