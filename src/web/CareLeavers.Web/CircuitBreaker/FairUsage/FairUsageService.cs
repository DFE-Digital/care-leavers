using System.Globalization;
using System.Text.Json;
using CareLeavers.Web.Configuration;
using Microsoft.Extensions.Options;

namespace CareLeavers.Web.CircuitBreaker.FairUsage;

public sealed class FairUsageService(IHttpContextAccessor accessor, IOptions<FairUsageOptions> options)
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);

    public bool ShouldLimitUsage(FairUsageType circuit)
    {
        if (accessor.HttpContext is null) throw new InvalidOperationException("HttpContext is NULL");
        HttpContext httpContext = accessor.HttpContext;

        return circuit switch
        {
            FairUsageType.AzureTranslation => ShouldLimitUsageAzureTranslation(httpContext),
            FairUsageType.PdfGenerator => ShouldLimitUsagePdfGenerator(httpContext),
            _ => throw new ArgumentOutOfRangeException(nameof(circuit))
        };
    }

    private bool ShouldLimitUsageAzureTranslation(HttpContext httpContext)
    {
        string? languageCode = httpContext.Request.RouteValues["languageCode"]?.ToString();

        if (languageCode is null or "en") return false;

        string? json = httpContext.Session.GetString(FairUsageOptions.AzureTranslationKey);
        List<string> translatedLanguages = json is null ? [] : JsonSerializer.Deserialize<List<string>>(json) ?? [];

        if (translatedLanguages.Contains(languageCode)) return false;
        if (translatedLanguages.Count >= options.Value.AzureTranslationLimit) return true;

        translatedLanguages.Add(languageCode);
        httpContext.Session.SetString(FairUsageOptions.AzureTranslationKey,
            JsonSerializer.Serialize(translatedLanguages, JsonSerializerOptions));
        httpContext.Session.SetString(FairUsageOptions.AzureTranslationTimeoutKey, GetFormattedTime());

        return false;
    }

    private static string GetFormattedTime()
    {
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
        DateTime timeout = DateTime.UtcNow.AddDays(1);
        if (timeZoneInfo.IsDaylightSavingTime(timeout)) timeout = timeout.AddHours(1);
        return timeout.ToString("h:mmtt", CultureInfo.CreateSpecificCulture("en-GB"));
    }

    private bool ShouldLimitUsagePdfGenerator(HttpContext httpContext)
    {
        int timesUsed = httpContext.Session.GetInt32(FairUsageOptions.PdfGeneratorKey) ?? 0;
        if (timesUsed >= options.Value.PdfGeneratorLimit) return true;
        httpContext.Session.SetInt32(FairUsageOptions.PdfGeneratorKey, timesUsed + 1);
        return false;
    }
}