#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster AS build
WORKDIR /src
COPY ["Dockord.Bot/Dockord.Bot.csproj", "Dockord.Bot/"]
RUN dotnet restore "Dockord.Bot/Dockord.Bot.csproj"
COPY . .
WORKDIR "/src/Dockord.Bot"
RUN dotnet build "Dockord.Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dockord.Bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dockord.Bot.dll"]