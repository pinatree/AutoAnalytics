#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["AutoAnalyticsServer/AutoAnalytics.WebPortal.WebSite.csproj", "AutoAnalyticsServer/"]
COPY ["AutoAnalyticsServer.DefaultService/AutoAnalytics.WebPortal.Business.csproj", "AutoAnalyticsServer.DefaultService/"]
COPY ["AutoAnalyticsServer.EFModel/AutoAnalytics.WebPortal.Domain.csproj", "AutoAnalyticsServer.EFModel/"]
COPY ["AutoAnalyticsServer.SqlServerRepository/AutoAnalytics.WebPortal.PostgresDAL.csproj", "AutoAnalyticsServer.SqlServerRepository/"]
COPY ["AutoAnalyticsServer.ISqlServerRepository/AutoAnalytics.WebPortal.IDAL.csproj", "AutoAnalyticsServer.ISqlServerRepository/"]
COPY ["AutoAnalyticsServer.IService/AutoAnalytics.WebPortal.IBusiness.csproj", "AutoAnalyticsServer.IService/"]
RUN dotnet restore "AutoAnalyticsServer/AutoAnalytics.WebPortal.WebSite.csproj"
COPY . .
WORKDIR "/src/AutoAnalyticsServer"
RUN dotnet build "AutoAnalytics.WebPortal.WebSite.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoAnalytics.WebPortal.WebSite.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoAnalytics.WebPortal.WebSite.dll"]