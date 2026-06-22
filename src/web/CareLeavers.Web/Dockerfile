# -- Build Node -- #
FROM node:26-alpine@sha256:a2dc166a387cc6ca1e62d0c8e265e49ca985d6e60abc9fe6e6c3d6ce8e63f606 AS build-node
WORKDIR /src

COPY ["web/CareLeavers.Web.Node/package.json", "."]
COPY ["web/CareLeavers.Web.Node/package-lock.json", "."]
RUN npm ci --ignore-scripts --omit=dev

COPY ["web/CareLeavers.Web.Node", "."]
RUN npm run build
# -- Build Node -- #

# -- Build .NET -- #
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine@sha256:fac7cce841f78faa4bca416fb4c636d1a129c09abd9b50e9b45664b95fd008a0 AS build-dotnet
WORKDIR /src

COPY ["web/CareLeavers.Web/CareLeavers.Web.csproj", "."]
COPY ["web/CareLeavers.Web/packages.lock.json", "."]
RUN dotnet restore CareLeavers.Web.csproj --use-lock-file

COPY ["web/CareLeavers.Web", "."]
RUN dotnet publish CareLeavers.Web.csproj --no-restore --configuration Release --output ./publish
# -- Build .NET -- #

# -- Run .NET -- #
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine@sha256:f03685b2735e0d3d25d6c60672e74b21bb6334f1402f71bae2d2cf02307163cd
RUN apk add --no-cache icu-libs icu-data-full tzdata
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
USER app
WORKDIR /src

COPY --from=build-node ["../CareLeavers.Web/wwwroot", "./wwwroot/"]
COPY --from=build-dotnet ["src/publish", "."]

EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "CareLeavers.Web.dll"]