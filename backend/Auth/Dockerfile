FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Auth/Auth.csproj", "Auth/"]
COPY ["WebUtil/WebUtil.csproj", "WebUtil/"]
COPY ["Data/MasterDataServer/MasterDataServer.csproj", "Data/MasterDataServer/"]
COPY ["Data/MasterDataTypes/MasterDataTypes.csproj", "Data/MasterDataTypes/"]
COPY ["WebShared/WebShared.csproj", "WebShared/"]
COPY ["BattleCore/BattleLogicShared/BattleLogicShared.csproj", "BattleCore/BattleLogicShared/"]
COPY ["Data/MasterDataClient/MasterDataClient.csproj", "Data/MasterDataClient/"]
RUN dotnet restore "Auth/Auth.csproj"
COPY . .
WORKDIR "/src/Auth"
RUN dotnet build "Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.dll"]