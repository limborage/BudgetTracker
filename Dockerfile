# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BudgetTracker.csproj", "."]
RUN dotnet restore "./BudgetTracker.csproj" 
COPY . .
WORKDIR "/src/."
RUN dotnet build "./BudgetTracker.csproj" -c $BUILD_CONFIGURATION -o /app/build
ENV PATH="$PATH:/root/.dotnet/tools"

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BudgetTracker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY BudgetTracker.csproj .
USER root
RUN apk upgrade && apk add dotnet8-sdk aspnetcore8-runtime && dotnet tool install --global dotnet-ef
ENTRYPOINT ["dotnet", "BudgetTracker.dll"]