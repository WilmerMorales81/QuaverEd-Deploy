# Use the official .NET 8 runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the official .NET 8 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["QuaverEd.API/QuaverEd.API.csproj", "QuaverEd.API/"]
RUN dotnet restore "QuaverEd.API/QuaverEd.API.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/QuaverEd.API"
RUN dotnet build "QuaverEd.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "QuaverEd.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Create the final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "QuaverEd.API.dll"]