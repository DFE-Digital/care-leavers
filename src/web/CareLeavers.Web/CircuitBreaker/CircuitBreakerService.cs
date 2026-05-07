using System.Text.Json;
using CareLeavers.Web.Configuration;
using Microsoft.Extensions.Options;

namespace CareLeavers.Web.CircuitBreaker;

public sealed class CircuitBreakerService(IHttpContextAccessor accessor, IOptions<CircuitBreakerOptions> options)
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);

    public bool ShouldBreakCircuit(CircuitBreakerType circuit)
    {
        if (accessor.HttpContext is null) throw new ArgumentNullException(nameof(accessor.HttpContext));

        return circuit switch
        {
            CircuitBreakerType.AzureTranslation => ShouldBreakCircuitAzureTranslation(),
            CircuitBreakerType.PdfGenerator => ShouldBreakCircuitPdfGenerator(),
            _ => throw new ArgumentOutOfRangeException(nameof(circuit), circuit, "Invalid Circuit Breaker Option")
        };
    }

    private bool ShouldBreakCircuitAzureTranslation()
    {
        string? languageCode = accessor.HttpContext?.Request.RouteValues["languageCode"]?.ToString();

        if (languageCode is null or "en") return false;

        string? json = accessor.HttpContext?.Session.GetString(CircuitBreakerOptions.AzureTranslationKey);
        List<string> translatedLanguages = json is null ? [] : JsonSerializer.Deserialize<List<string>>(json) ?? [];

        if (translatedLanguages.Contains(languageCode)) return false;
        if (translatedLanguages.Count >= options.Value.AzureTranslationLimit) return true;

        translatedLanguages.Add(languageCode);
        accessor.HttpContext?.Session.SetString(CircuitBreakerOptions.AzureTranslationKey,
            JsonSerializer.Serialize(translatedLanguages, JsonSerializerOptions));

        return false;
    }

    private bool ShouldBreakCircuitPdfGenerator()
    {
        int timesUsed = accessor.HttpContext?.Session.GetInt32(CircuitBreakerOptions.PdfGeneratorKey) ?? 0;
        if (timesUsed >= options.Value.PdfGeneratorLimit) return true;
        accessor.HttpContext?.Session.SetInt32(CircuitBreakerOptions.PdfGeneratorKey, timesUsed + 1);
        return false;
    }
}