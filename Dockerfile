# Use the official ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore any dependencies
COPY ["ChessFrontend8/ChessFrontend8.csproj", "ChessFrontend8/"]
RUN dotnet restore "ChessFrontend8/ChessFrontend8.csproj"

# Copy the entire project and build it
COPY . .
WORKDIR "/src/ChessFrontend8"
RUN dotnet build "ChessFrontend8.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ChessFrontend8.csproj" -c Release -o /app/publish

# Final stage: copy the published application to the base image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChessFrontend8.dll"]
