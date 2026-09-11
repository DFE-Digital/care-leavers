# -- Build Node -- #
FROM node:26-alpine@sha256:ef24c5053d50fdc3e4e56eb4e7ddb7861874ab0fdc797046ba897581deb8e868 AS build-node
WORKDIR /src

COPY ["web/CareLeavers.Web.Node/package.json", "."]
COPY ["web/CareLeavers.Web.Node/package-lock.json", "."]
RUN npm ci --ignore-scripts --omit=dev

COPY ["web/CareLeavers.Web.Node", "."]
RUN npm run build
# -- Build Node -- #

# -- Build .NET -- #
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine@sha256:4ac537e13e2f55d1d588ed3e618cb0cb6b82dd8deb17830de43d5086fbde958b AS build-dotnet
WORKDIR /src

COPY ["web/CareLeavers.Web/CareLeavers.Web.csproj", "."]
COPY ["web/CareLeavers.Web/packages.lock.json", "."]
RUN dotnet restore CareLeavers.Web.csproj --use-lock-file

COPY ["web/CareLeavers.Web", "."]
RUN dotnet publish CareLeavers.Web.csproj --no-restore --configuration Release --output ./publish
# -- Build .NET -- #

# -- Run .NET -- #
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine@sha256:6bb0fab0ef31f44f710a668c39c2263ae810f5adf868afa34cbd86815912c7fe
RUN apk add --no-cache icu-libs icu-data-full tzdata
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
USER app
WORKDIR /src

COPY --from=build-node ["../CareLeavers.Web/wwwroot", "./wwwroot/"]
COPY --from=build-dotnet ["src/publish", "."]

EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "CareLeavers.Web.dll"]