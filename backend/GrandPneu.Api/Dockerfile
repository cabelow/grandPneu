# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia solution e projetos
COPY grandPneu_backend.sln .
COPY GrandPneu.Api/GrandPneu.Api.csproj GrandPneu.Api/
COPY GrandPneu.Application/GrandPneu.Application.csproj GrandPneu.Application/
COPY GrandPneu.Domain/GrandPneu.Domain.csproj GrandPneu.Domain/
COPY GrandPneu.Infrastructure/GrandPneu.Infrastructure.csproj GrandPneu.Infrastructure/

# Restore
RUN dotnet restore

# Copia o restante do código
COPY . .

# Build
WORKDIR /src/GrandPneu.Api
RUN dotnet publish -c Release -o /app/publish

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "GrandPneu.Api.dll"]
