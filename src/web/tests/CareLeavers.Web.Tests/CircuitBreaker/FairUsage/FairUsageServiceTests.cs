using System.Text.Json;
using CareLeavers.Web.CircuitBreaker.FairUsage;
using CareLeavers.Web.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace CareLeavers.Web.Tests.CircuitBreaker.FairUsage;

public class FairUsageServiceTests
{
    private IHttpContextAccessor _httpContextAccessor;
    private HttpContext _httpContext;
    private IOptions<FairUsageOptions> _circuitBreakerOptions;
    private FairUsageService _fairUsageService;

    [SetUp]
    public void Init()
    {
        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        _httpContext = new DefaultHttpContext();
        _httpContext.Session = new MockSession();
        _httpContextAccessor.HttpContext = _httpContext;

        _circuitBreakerOptions = Options.Create(new FairUsageOptions
        {
            AzureTranslationLimit = 2,
            PdfGeneratorLimit = 2
        });

        _fairUsageService = new FairUsageService(_httpContextAccessor, _circuitBreakerOptions);
    }

    [Test]
    public void ShouldBreakCircuit_When_HttpContextIsNull_Throws_InvalidOperationException()
    {
        _httpContextAccessor.HttpContext = null;

        Assert.That((Func<bool>)CircuitBreaker,
            Throws.TypeOf<InvalidOperationException>().With.Message.EqualTo("HttpContext is NULL"));
        return;

        bool CircuitBreaker() => _fairUsageService.ShouldLimitUsage(FairUsageType.AzureTranslation);
    }

    [Test]
    public void ShouldBreakCircuit_When_CircuitBreakerType_IsNull_Throws_ArgumentOutOfRangeException()
    {
        Assert.That((Func<bool>)CircuitBreaker,
            Throws.TypeOf<ArgumentOutOfRangeException>().With.Message
                .EqualTo("Specified argument was out of the range of valid values. (Parameter 'circuit')"));
        return;

        bool CircuitBreaker() => _fairUsageService.ShouldLimitUsage((FairUsageType)2);
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_LanguageCode_IsNull_Returns_False()
    {
        _httpContext.Request.RouteValues["languageCode"] = null;

        bool result = _fairUsageService.ShouldLimitUsage(FairUsageType.AzureTranslation);

        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_LanguageCode_IsEnglish_Returns_False()
    {
        _httpContext.Request.RouteValues["languageCode"] = "en";

        bool result = _fairUsageService.ShouldLimitUsage(FairUsageType.AzureTranslation);

        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_JsonIsNull_Returns_False()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";

        bool result = _fairUsageService.ShouldLimitUsage(FairUsageType.AzureTranslation);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(_httpContext.Session.GetString(FairUsageOptions.AzureTranslationKey),
                Is.EqualTo(JsonSerializer.Serialize(new List<string> { "sv" })));
        }
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_JsonValueIsNull_Returns_False()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _httpContext.Session.SetString(FairUsageOptions.AzureTranslationKey, "null");

        bool result = _fairUsageService.ShouldLimitUsage(FairUsageType.AzureTranslation);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(_httpContext.Session.GetString(FairUsageOptions.AzureTranslationKey),
                Is.EqualTo(JsonSerializer.Serialize(new List<string> { "sv" })));
        }
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_LanguagePreviouslyTranslated_Returns_False()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _httpContext.Session.SetString(FairUsageOptions.AzureTranslationKey,
            JsonSerializer.Serialize(new List<string> { "sv" }));

        bool result = _fairUsageService.ShouldLimitUsage(FairUsageType.AzureTranslation);

        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_AtLimit_But_LanguagePreviouslyTranslated_Returns_False()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _httpContext.Session.SetString(FairUsageOptions.AzureTranslationKey,
            JsonSerializer.Serialize(new List<string> { "sv", "fr" }));

        bool result = _fairUsageService.ShouldLimitUsage(FairUsageType.AzureTranslation);

        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_LimitIsReached_Returns_True()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _httpContext.Session.SetString(FairUsageOptions.AzureTranslationKey,
            JsonSerializer.Serialize(new List<string> { "es", "fr" }));

        bool result = _fairUsageService.ShouldLimitUsage(FairUsageType.AzureTranslation);

        Assert.That(result, Is.True);
    }

    [Test]
    public void ShouldBreakCircuit_AzureTranslation_When_LimitIsNotReached_Returns_False_And_UpdatesSession()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _httpContext.Session.SetString(FairUsageOptions.AzureTranslationKey,
            JsonSerializer.Serialize(new List<string> { "es" }));

        bool result = _fairUsageService.ShouldLimitUsage(FairUsageType.AzureTranslation);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(_httpContext.Session.GetString(FairUsageOptions.AzureTranslationKey),
                Is.EqualTo(JsonSerializer.Serialize(new List<string> { "es", "sv" })));
        }
    }

    [Test]
    public void ShouldBreakCircuit_PdfGenerator_When_FirstUsage_Returns_False_And_UpdatesSession()
    {
        bool result = _fairUsageService.ShouldLimitUsage(FairUsageType.PdfGenerator);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(_httpContext.Session.GetInt32(FairUsageOptions.PdfGeneratorKey), Is.EqualTo(1));
        }
    }

    [Test]
    public void ShouldBreakCircuit_PdfGenerator_When_BelowLimit_Returns_False_And_UpdatesSession()
    {
        _httpContext.Session.SetInt32(FairUsageOptions.PdfGeneratorKey, 1);

        bool result = _fairUsageService.ShouldLimitUsage(FairUsageType.PdfGenerator);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(_httpContext.Session.GetInt32(FairUsageOptions.PdfGeneratorKey), Is.EqualTo(2));
        }
    }

    [Test]
    public void ShouldBreakCircuit_PdfGenerator_When_LimitIsReached_Returns_True()
    {
        _httpContext.Session.SetInt32(FairUsageOptions.PdfGeneratorKey, 2);

        bool result = _fairUsageService.ShouldLimitUsage(FairUsageType.PdfGenerator);

        Assert.That(result, Is.True);
    }
}