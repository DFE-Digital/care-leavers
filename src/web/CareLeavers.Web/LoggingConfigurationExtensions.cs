using System.Diagnostics.CodeAnalysis;
using Microsoft.ApplicationInsights.Extensibility;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.OpenTelemetry;

namespace CareLeavers.Web;

[ExcludeFromCodeCoverage(Justification = "Configuration only")]
public static class LoggingConfigurationExtensions
{
    public static LoggerConfiguration ConfigureLogging(
        this LoggerConfiguration loggerConfig,
        string? appInsightsConnectionString,
        string? otelEndpoint)
    {
        var config = loggerConfig
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .WriteTo.Console()
            .WriteTo.OpenTelemetry(options =>
            {
                options.Endpoint = otelEndpoint ?? "http://otel-collector:4318";
                options.Protocol = OtlpProtocol.HttpProtobuf;
            })
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