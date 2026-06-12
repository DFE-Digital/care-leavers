# -- Build Node -- #
FROM node:26-alpine@sha256:3ad34ca6292aec4a91d8ddeb9229e29d9c2f689efd0dd242860889ac71842eba AS build-node
WORKDIR /src

RUN npm install --ignore-scripts --global corepack@0.35.0
RUN npm install --ignore-scripts --global gulp-cli@3.1.0

COPY ["web/CareLeavers.Web/package.json", "."]
COPY ["web/CareLeavers.Web/yarn.lock", "."]
RUN yarn install --ignore-scripts

COPY ["web/CareLeavers.Web/Gulpfile.js", "."]
COPY ["web/CareLeavers.Web/AssetSrc/", "./AssetSrc/"]
RUN gulp dev
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

COPY --from=build-node ["src/wwwroot", "./wwwroot/"]
COPY --from=build-dotnet ["src/publish", "."]

EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "CareLeavers.Web.dll"]