FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CovidLAMap.API/CovidLAMap.API.csproj", "CovidLAMap.API/"]
COPY ["CovidLAMap.Serivices/CovidLAMap.Services.csproj", "CovidLAMap.Serivices/"]
COPY ["CovidLAMap.Data/CovidLAMap.Data.csproj", "CovidLAMap.Data/"]
COPY ["CovidLAMap.Core/CovidLAMap.Core.csproj", "CovidLAMap.Core/"]
RUN dotnet restore "CovidLAMap.API/CovidLAMap.API.csproj"
COPY . .
WORKDIR "/src/CovidLAMap.API"
RUN dotnet build "CovidLAMap.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CovidLAMap.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CovidLAMap.API.dll"]