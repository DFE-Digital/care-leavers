# -- Build Node -- #
#    Image: Node 26 Alpine
FROM node@sha256:144769ec3f32e8ee36b3cfde91e82bee25d9367b20f31a151f3f7eea3a2a8541 AS build-node
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
#    Image: .NET 8 Alpine
FROM mcr.microsoft.com/dotnet/sdk@sha256:d9f4f4a5d99a43799b500ee1365c370e3233822fbe7d43666715d9b5b5cda2ab AS build-dotnet
WORKDIR /src

COPY ["web/CareLeavers.Web/CareLeavers.Web.csproj", "."]
COPY ["web/CareLeavers.Web/packages.lock.json", "."]
RUN dotnet restore CareLeavers.Web.csproj --use-lock-file

COPY ["web/CareLeavers.Web", "."]
RUN dotnet publish CareLeavers.Web.csproj --no-restore --configuration Release --output ./publish
# -- Build .NET -- #

# -- Run .NET -- #
#    Image: .NET 8 Alpine
FROM mcr.microsoft.com/dotnet/aspnet@sha256:4718c6b44771e710f91d6129912cb4fa3b90024ec4e23b3f8ce89822fba612fd
RUN apk add --no-cache icu-libs icu-data-full tzdata
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
USER app
WORKDIR /src

COPY --from=build-node ["src/wwwroot", "./wwwroot/"]
COPY --from=build-dotnet ["src/publish", "."]

EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "CareLeavers.Web.dll"]