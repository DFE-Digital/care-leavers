# -- Build Node -- #
FROM node:26-alpine@sha256:aadf416b2cdce311a8811ba3f0608a61b77dbf997500e2eafe781b51f6a0b019 AS build-node
WORKDIR /src

COPY ["web/CareLeavers.Web.Node/package.json", "."]
COPY ["web/CareLeavers.Web.Node/package-lock.json", "."]
RUN npm ci --ignore-scripts --omit=dev

COPY ["web/CareLeavers.Web.Node", "."]
RUN npm run build
# -- Build Node -- #

# -- Build .NET -- #
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine@sha256:620e765fe18186c08399f7aa978f79f04b6bbf0ee1b3b8a91e2d5c9619e59da1 AS build-dotnet
WORKDIR /src

COPY ["web/CareLeavers.Web/CareLeavers.Web.csproj", "."]
COPY ["web/CareLeavers.Web/packages.lock.json", "."]
RUN dotnet restore CareLeavers.Web.csproj --use-lock-file

COPY ["web/CareLeavers.Web", "."]
RUN dotnet publish CareLeavers.Web.csproj --no-restore --configuration Release --output ./publish
# -- Build .NET -- #

# -- Run .NET -- #
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine@sha256:c4b29bf368004ad9076c1ab9bc91fb373561e3905b4345637e14e8b8c57e3be8
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