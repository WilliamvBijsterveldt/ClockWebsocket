# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./ClockWebsocket/ClockWebsocket.csproj" --disable-parallel
RUN dotnet restore "./ClockWebsocket/ClockWebsocket.csproj" -c release -o /app --no-restore

# Serve stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5209

ENTRYPOINT ["dotnet", "ClockWebsocket.dll"]