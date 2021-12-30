#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Board/Board.csproj", "Board/"]
COPY ["WebUtil/WebUtil.csproj", "WebUtil/"]
COPY ["Protocols/Protocols.csproj", "Protocols/"]
RUN dotnet restore "Board/Board.csproj"
COPY . .
WORKDIR "/src/Board"
RUN dotnet build "Board.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Board.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Board.dll"]