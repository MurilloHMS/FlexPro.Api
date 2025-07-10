FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

COPY ["FlexPro.Api/FlexPro.Api.csproj", "FlexPro.Api/"]
RUN dotnet restore "FlexPro.Api/FlexPro.Api.csproj"

# Copiar a pasta Templates separadamente
COPY FlexPro.Api/Infrastructure/Templates /src/Infrastructure/Templates

COPY . .
WORKDIR "/src/FlexPro.Api"
RUN dotnet build "FlexPro.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FlexPro.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
RUN apk add --no-cache icu-libs

COPY --from=publish /app/publish .
COPY --from=build /src/Infrastructure/Templates /app/Infrastructure/Templates
ENTRYPOINT ["dotnet", "FlexPro.Api.dll"]
