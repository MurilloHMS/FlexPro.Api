# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["FlexPro.Api/FlexPro.Api.csproj", "FlexPro.Api/"]
RUN dotnet restore "./FlexPro.Api/FlexPro.Api.csproj"

# Copiar a pasta Templates separadamente
COPY FlexPro.Api/Infrastructure/Templates /src/Infrastructure/Templates

COPY . .
WORKDIR "/src/FlexPro.Api"
RUN dotnet build "./FlexPro.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FlexPro.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/Infrastructure/Templates /app/Infrastructure/Templates
ENTRYPOINT ["dotnet", "FlexPro.Api.dll"]
