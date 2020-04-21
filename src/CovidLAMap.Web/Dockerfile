#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
WORKDIR /src
COPY ["CovidLAMap.Web/CovidLAMap.Web.csproj", "CovidLAMap.Web/"]
COPY ["CovidLAMap.Serivices/CovidLAMap.Services.csproj", "CovidLAMap.Serivices/"]
COPY ["CovidLAMap.Data/CovidLAMap.Data.csproj", "CovidLAMap.Data/"]
COPY ["CovidLAMap.Core/CovidLAMap.Core.csproj", "CovidLAMap.Core/"]
RUN dotnet restore "CovidLAMap.Web/CovidLAMap.Web.csproj"
COPY . .
WORKDIR "/src/CovidLAMap.Web"
RUN dotnet build "CovidLAMap.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CovidLAMap.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CovidLAMap.Web.dll"]