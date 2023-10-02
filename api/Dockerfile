FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

WORKDIR /app

EXPOSE 80

WORKDIR /src
COPY api.csproj .
RUN dotnet restore api.csproj
COPY . .

RUN dotnet build api.csproj -c Release -o /app/build

FROM build AS publish

RUN dotnet publish api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS final

WORKDIR /app

RUN adduser --disabled-password -u 10001 \
    --home /app \
    --gecos '' appuser && chown -R appuser:appuser /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]