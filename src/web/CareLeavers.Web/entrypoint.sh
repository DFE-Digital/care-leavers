#!/bin/sh

# Launch Splunk OTEL collector in the background
/usr/local/bin/otelcol --config=/otel-collector-config.yaml &

# Execute whatever command was passed in (e.g., "dotnet", "CareLeavers.Web.dll")
exec "$@"