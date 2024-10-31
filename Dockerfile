FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY HalloweenApp/HalloweenApp.csproj HalloweenApp/
RUN dotnet restore HalloweenApp/HalloweenApp.csproj

COPY . . 
WORKDIR /src/HalloweenApp
RUN dotnet build HalloweenApp.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish HalloweenApp.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HalloweenApp.dll"] 
