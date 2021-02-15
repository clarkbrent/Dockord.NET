#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WebClients/Dockord/Dockord.Bot/Dockord.Bot.csproj", "WebClients/Dockord/Dockord.Bot/"]
RUN dotnet restore "WebClients/Dockord/Dockord.Bot/Dockord.Bot.csproj"
COPY . .
WORKDIR "/src/WebClients/Dockord/Dockord.Bot"
RUN dotnet build "Dockord.Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dockord.Bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dockord.Bot.dll"]