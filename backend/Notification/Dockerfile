FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Notification/Notification.csproj", "Notification/"]
COPY ["WebUtil/WebUtil.csproj", "WebUtil/"]
COPY ["Data/MasterDataServer/MasterDataServer.csproj", "Data/MasterDataServer/"]
COPY ["Data/MasterDataTypes/MasterDataTypes.csproj", "Data/MasterDataTypes/"]
COPY ["WebShared/WebShared.csproj", "WebShared/"]
COPY ["BattleCore/BattleLogicShared/BattleLogicShared.csproj", "BattleCore/BattleLogicShared/"]
COPY ["Data/MasterDataClient/MasterDataClient.csproj", "Data/MasterDataClient/"]
RUN dotnet restore "Notification/Notification.csproj"
COPY . .
WORKDIR "/src/Notification"
RUN dotnet build "Notification.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notification.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.dll"]