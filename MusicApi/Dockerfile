# Use ASP.NET Core runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# ENV env-fatima-01=value

# Use SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MusicApi.csproj", "./"]
RUN dotnet restore "./MusicApi.csproj"

# Copy the rest of the source code and build
COPY . .
RUN dotnet build "./MusicApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./MusicApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage: Use runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicApi.dll"]
