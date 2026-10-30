# -- Build Node -- #
FROM node:26-alpine@sha256:0b36e8c136b94cd4fcf02188228e76c31ad5872eef3fec8cbd2eee500cfd9e80 AS build-node
WORKDIR /src

COPY ["web/CareLeavers.Web.Node/package.json", "."]
COPY ["web/CareLeavers.Web.Node/package-lock.json", "."]
RUN npm ci --ignore-scripts --omit=dev

COPY ["web/CareLeavers.Web.Node", "."]
RUN npm run build
# -- Build Node -- #

# -- Build .NET -- #
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine@sha256:cc02b814237a76c4735fb6eeee87181597b5e56f2ce458a435e527674d91f4d7 AS build-dotnet
WORKDIR /src

COPY ["web/CareLeavers.Web/CareLeavers.Web.csproj", "."]
COPY ["web/CareLeavers.Web/packages.lock.json", "."]
RUN dotnet restore CareLeavers.Web.csproj --use-lock-file

COPY ["web/CareLeavers.Web", "."]
RUN dotnet publish CareLeavers.Web.csproj --no-restore --configuration Release --output ./publish
# -- Build .NET -- #

# -- Run .NET -- #
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine@sha256:254fe017495996d3abcc487ffacdcce2b73affb380d3af87af12c8dff41fe0b0
RUN apk add --no-cache icu-libs icu-data-full tzdata gcompat
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /src

COPY --from=quay.io/signalfx/splunk-otel-collector:latest /otelcol /usr/local/bin/otelcol

COPY ["web/CareLeavers.Web/entrypoint.sh", "/entrypoint.sh"]
RUN chmod +x /entrypoint.sh
COPY  ["web/CareLeavers.Web/otel-config.yaml", "/otel-collector-config.yaml"]

COPY --from=build-node ["../CareLeavers.Web/wwwroot", "./wwwroot/"]
COPY --from=build-dotnet ["src/publish", "."]

USER app

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["/entrypoint.sh"]
CMD ["dotnet", "CareLeavers.Web.dll"]