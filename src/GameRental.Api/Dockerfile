FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY src/GameRental.Core/GameRental.Core.csproj src/GameRental.Core/
COPY src/GameRental.Api/GameRental.Api.csproj src/GameRental.Api/
RUN dotnet restore src/GameRental.Api/GameRental.Api.csproj
COPY src/ src/
RUN dotnet publish src/GameRental.Api/GameRental.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "GameRental.Api.dll"]