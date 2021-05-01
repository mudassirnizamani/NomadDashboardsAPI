#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NomadDashboardsAPI/NomadDashboardsAPI.csproj", "NomadDashboardsAPI/"]
RUN dotnet restore "NomadDashboardsAPI/NomadDashboardsAPI.csproj"
COPY . .
WORKDIR "/src/NomadDashboardsAPI"
RUN dotnet build "NomadDashboardsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NomadDashboardsAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NomadDashboardsAPI.dll"]