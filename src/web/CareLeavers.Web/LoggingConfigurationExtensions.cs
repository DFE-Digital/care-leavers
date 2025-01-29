using System.Diagnostics.CodeAnalysis;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using Serilog.Events;

namespace CareLeavers.Web;

[ExcludeFromCodeCoverage(Justification = "Configuration only")]
public static class LoggingConfigurationExtensions
{
    public static LoggerConfiguration ConfigureLogging(
        this LoggerConfiguration loggerConfig,
        string? appInsightsConnectionString)
    {
        var config = loggerConfig
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .WriteTo.Console()
            .Enrich.FromLogContext();

        if (!string.IsNullOrEmpty(appInsightsConnectionString))
        {
            config = config.WriteTo.ApplicationInsights(new TelemetryConfiguration
            {
                ConnectionString = appInsightsConnectionString
            }, TelemetryConverter.Traces);
        }

        return config;
    }
}