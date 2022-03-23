#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:3.0 AS base
WORKDIR /api
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Accelerator.API/Accelerator.API.csproj", "Accelerator.API/"]
COPY ["Accelerator.Contracts/Accelerator.Contracts.csproj", "Accelerator.Contracts/"]
RUN dotnet restore "Accelerator.API/Accelerator.API.csproj"
COPY . .
WORKDIR "/src/Accelerator.API"
RUN dotnet build "Accelerator.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accelerator.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /api
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/api \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true