#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ECommerce.Basket.Api/ECommerce.Basket.Api.csproj", "ECommerce.Basket.Api/"]
COPY ["ECommerce.Basket.Data/ECommerce.Basket.Data.csproj", "ECommerce.Basket.Data/"]
COPY ["ECommerce.Basket.Models/ECommerce.Basket.Models.csproj", "ECommerce.Basket.Models/"]
COPY ["ECommerce.Basket.Business/ECommerce.Basket.Business.csproj", "ECommerce.Basket.Business/"]
RUN dotnet restore "ECommerce.Basket.Api/ECommerce.Basket.Api.csproj"
COPY . .
WORKDIR "/src/ECommerce.Basket.Api"
RUN dotnet build "ECommerce.Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.Basket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Basket.Api.dll"]