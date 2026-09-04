# -- Build Node -- #
FROM node:26-alpine@sha256:2d984a15c9b54fd0aeb608b8e0d0d83529eb34d2966db27a1fb4f1edc3d298a3 AS build-node
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
RUN apk add --no-cache icu-libs icu-data-full tzdata
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
USER app
WORKDIR /src

COPY --from=build-node ["../CareLeavers.Web/wwwroot", "./wwwroot/"]
COPY --from=build-dotnet ["src/publish", "."]

EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "CareLeavers.Web.dll"]