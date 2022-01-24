#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0-bullseye-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["Dockord.Bot/Dockord.Bot.csproj", "Dockord.Bot/"]
COPY ["Dockord.Library/Dockord.Library.csproj", "Dockord.Library/"]
COPY ["tests/Dockord.Bot.Tests/Dockord.Bot.UnitTests.csproj", "tests/Dockord.Bot.Tests/"]
COPY ["tests/Dockord.Library.Tests/Dockord.Library.UnitTests.csproj", "tests/Dockord.Library.Tests/"]
RUN dotnet restore "Dockord.Bot/Dockord.Bot.csproj"
RUN dotnet restore "Dockord.Library/Dockord.Library.csproj"
RUN dotnet restore "tests/Dockord.Bot.Tests/Dockord.Bot.UnitTests.csproj"
RUN dotnet restore "tests/Dockord.Library.Tests/Dockord.Library.UnitTests.csproj"
COPY . .
WORKDIR /src/Dockord.Bot
RUN dotnet build "Dockord.Bot.csproj" -c Release -o /app/build

FROM build AS tests
WORKDIR /src/tests
RUN dotnet test --logger:trx "Dockord.Bot.Tests/Dockord.Bot.UnitTests.csproj"
RUN dotnet test --logger:trx "Dockord.Library.Tests/Dockord.Library.UnitTests.csproj"

FROM build AS publish
RUN dotnet publish "Dockord.Bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dockord.Bot.dll"]