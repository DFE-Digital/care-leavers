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
    public void ShouldBreakCircuitAzureTranslation_WhenLanguageIsEnglish_ReturnsFalse()
    {
        _httpContext.Request.RouteValues["languageCode"] = "en";
        
        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);
        
        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldBreakCircuitAzureTranslation_WhenLanguageIsNotEnglishAndLimitNotReached_ReturnsFalseAndAddsToSession()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        
        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);
        
        Assert.That(result, Is.False);
        
        string? json = _httpContext.Session.GetString(CircuitBreakerOptions.AzureTranslationKey);
        Assert.That(json, Is.Not.Null);
        List<string>? translatedLanguages = JsonSerializer.Deserialize<List<string>>(json);
        Assert.That(translatedLanguages, Is.Not.Null);
        Assert.That(translatedLanguages, Is.Not.Empty);
        Assert.That(translatedLanguages, Contains.Item("sv"));
    }

    [Test]
    public void ShouldBreakCircuitAzureTranslation_WhenLanguagePreviouslyTranslated_ReturnsFalse()
    {
        _httpContext.Request.RouteValues["languageCode"] = "sv";
        _httpContext.Session.SetString(CircuitBreakerOptions.AzureTranslationKey, JsonSerializer.Serialize(new List<string> { "sv" }));
        
        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);
        
        Assert.That(result, Is.False);
    }

    [Test]
    public void ShouldBreakCircuitAzureTranslation_WhenLimitReached_ReturnsTrue()
    {
        _httpContext.Request.RouteValues["languageCode"] = "es";
        _httpContext.Session.SetString(CircuitBreakerOptions.AzureTranslationKey, JsonSerializer.Serialize(new List<string> { "sv", "fr" }));
        
        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.AzureTranslation);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void ShouldBreakCircuitPdfGenerator_WhenUsageBelowLimit_ReturnsFalseAndIncrementsCount()
    {
        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.PdfGenerator);
        
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.False);
            Assert.That(_httpContext.Session.GetInt32(CircuitBreakerOptions.PdfGeneratorKey), Is.EqualTo(1));
        }
    }

    [Test]
    public void ShouldBreakCircuitPdfGenerator_WhenUsageAtLimit_ReturnsTrue()
    {
        _httpContext.Session.SetInt32(CircuitBreakerOptions.PdfGeneratorKey, 2);
        
        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.PdfGenerator);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void ShouldBreakCircuitPdfGenerator_WhenUsageExceedsLimit_ReturnsTrue()
    {
        _httpContext.Session.SetInt32(CircuitBreakerOptions.PdfGeneratorKey, 3);
        
        bool result = _circuitBreakerService.ShouldBreakCircuit(CircuitBreakerType.PdfGenerator);
        
        Assert.That(result, Is.True);
    }
}
