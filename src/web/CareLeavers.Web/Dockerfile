FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY web/CareLeavers.Web .
RUN dotnet publish "CareLeavers.Web.csproj" -c Release -o /app/out --sc false
RUN dotnet dev-certs https -ep /app/out/aspnetapp.pfx -p e2e

FROM alpine:latest AS security_provider
RUN addgroup -S nonroot && adduser -S nonroot -G nonroot

FROM node:20 AS node
WORKDIR /src
COPY "web/CareLeavers.Web/package.json" "web/CareLeavers.Web/yarn.lock" "web/CareLeavers.Web/Gulpfile.js" ./
COPY "web/CareLeavers.Web/AssetSrc/" "AssetSrc/"
RUN yarn install --ignore-scripts
RUN npm install --ignore-scripts -g gulp
RUN gulp dev

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/out .
COPY --from=node /src/wwwroot ./wwwroot
COPY --from=security_provider /etc/passwd /etc/passwd
USER nonroot
ENTRYPOINT ["dotnet", "CareLeavers.Web.dll"]
