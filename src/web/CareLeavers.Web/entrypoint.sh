#!/bin/sh

# Launch Splunk OTEL collector in the background
/usr/local/bin/otelcol --config=/otel-collector-config.yaml > /proc/1/fd/1 2>&1 &

# Execute whatever command was passed in (e.g., "dotnet", "CareLeavers.Web.dll")
exec "$@"