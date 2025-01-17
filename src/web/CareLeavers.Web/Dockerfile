FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM node:20 AS node
WORKDIR /src
COPY "web/CareLeavers.Web/package.json" "web/CareLeavers.Web/yarn.lock" "web/CareLeavers.Web/Gulpfile.js" ./
COPY "web/CareLeavers.Web/AssetSrc/" "AssetSrc/"
RUN yarn install
RUN npm install -g gulp
RUN gulp dev

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["web/CareLeavers.Web/CareLeavers.Web.csproj", "CareLeavers.Web/"]
RUN dotnet restore "CareLeavers.Web/CareLeavers.Web.csproj"
COPY . .
WORKDIR "/src/web/CareLeavers.Web"
RUN dotnet build "CareLeavers.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CareLeavers.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=node /src/wwwroot/ wwwroot
ENTRYPOINT ["dotnet", "CareLeavers.Web.dll"]
