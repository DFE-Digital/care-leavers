using System.Globalization;
using System.Text.Json;
using CareLeavers.Web.Configuration;
using Microsoft.Extensions.Options;

namespace CareLeavers.Web.CircuitBreaker;

public sealed class CircuitBreakerService(IHttpContextAccessor accessor, IOptions<CircuitBreakerOptions> options)
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);

    public bool ShouldBreakCircuit(CircuitBreakerType circuit)
    {
        if (accessor.HttpContext is null) throw new InvalidOperationException("HttpContext is NULL");
        HttpContext httpContext = accessor.HttpContext;

        return circuit switch
        {
            CircuitBreakerType.AzureTranslation => ShouldBreakCircuitAzureTranslation(httpContext),
            CircuitBreakerType.PdfGenerator => ShouldBreakCircuitPdfGenerator(httpContext),
            _ => throw new ArgumentOutOfRangeException(nameof(circuit))
        };
    }

    private bool ShouldBreakCircuitAzureTranslation(HttpContext httpContext)
    {
        string? languageCode = httpContext.Request.RouteValues["languageCode"]?.ToString();

        if (languageCode is null or "en") return false;

        string? json = httpContext.Session.GetString(CircuitBreakerOptions.AzureTranslationKey);
        List<string> translatedLanguages = json is null ? [] : JsonSerializer.Deserialize<List<string>>(json) ?? [];

        if (translatedLanguages.Contains(languageCode)) return false;
        if (translatedLanguages.Count >= options.Value.AzureTranslationLimit) return true;

        translatedLanguages.Add(languageCode);
        httpContext.Session.SetString(CircuitBreakerOptions.AzureTranslationKey,
            JsonSerializer.Serialize(translatedLanguages, JsonSerializerOptions));
        httpContext.Session.SetString(CircuitBreakerOptions.AzureTranslationTimeoutKey, GetFormattedTime());

        return false;
    }

    private static string GetFormattedTime()
    {
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
        DateTime timeout = DateTime.UtcNow.AddDays(1);
        if (timeZoneInfo.IsDaylightSavingTime(timeout)) timeout = timeout.AddHours(1);
        return timeout.ToString("h:mmtt", CultureInfo.CreateSpecificCulture("en-GB"));
    }

    private bool ShouldBreakCircuitPdfGenerator(HttpContext httpContext)
    {
        int timesUsed = httpContext.Session.GetInt32(CircuitBreakerOptions.PdfGeneratorKey) ?? 0;
        if (timesUsed >= options.Value.PdfGeneratorLimit) return true;
        httpContext.Session.SetInt32(CircuitBreakerOptions.PdfGeneratorKey, timesUsed + 1);
        return false;
    }
}