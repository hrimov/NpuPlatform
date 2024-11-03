FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY *.sln ./
COPY NpuBackend.Api/NpuBackend.Api.csproj NpuBackend.Api/
COPY NpuBackend.Data/NpuBackend.Data.csproj NpuBackend.Data/
COPY NpuBackend.Domain/NpuBackend.Domain.csproj NpuBackend.Domain/
COPY NpuBackend.Services/NpuBackend.Services.csproj NpuBackend.Services/
COPY NpuBackend.Infrastructure/NpuBackend.Infrastructure.csproj NpuBackend.Infrastructure/
COPY NpuBackend.Tests/NpuBackend.Tests.csproj NpuBackend.Tests/

RUN dotnet restore

COPY . ./
RUN dotnet publish NpuBackend.Api/NpuBackend.Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "NpuBackend.Api.dll"]
