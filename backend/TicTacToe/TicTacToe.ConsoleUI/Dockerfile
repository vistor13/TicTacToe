# See the article at https://aka.ms/customizecontainer to learn how to configure the debug container 
# and how Visual Studio uses this Dockerfile to create images for faster debugging.

# This stage is used when running from Visual Studio in fast mode (default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

# This stage is used for building the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TicTacToe.ConsoleUI/TicTacToe.ConsoleUI.csproj", "TicTacToe.ConsoleUI/"]
RUN dotnet restore "./TicTacToe.ConsoleUI/TicTacToe.ConsoleUI.csproj"
COPY . .
WORKDIR "/src/TicTacToe.ConsoleUI"
RUN dotnet build "./TicTacToe.ConsoleUI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used for publishing the service project, which will be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TicTacToe.ConsoleUI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from Visual Studio in normal mode 
# (default when Debug configuration is not used)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToe.ConsoleUI.dll"]