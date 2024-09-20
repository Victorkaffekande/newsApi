FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["newsApi.csproj", "./"]
RUN dotnet restore "newsApi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "newsApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "newsApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

EXPOSE 5000
# Set the ASP.NET Core URLs environment variable to listen on port 5000
ENV ASPNETCORE_URLS=http://+:5000
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "newsApi.dll"]
