FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DevOPs_Assignment/DevOPs_Assignment.csproj", "DevOPs_Assignment/"]
RUN dotnet restore "DevOPs_Assignment/DevOPs_Assignment.csproj"
COPY . .
WORKDIR "/src/DevOPs_Assignment"
RUN dotnet build "./DevOPs_Assignment.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DevOPs_Assignment.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevOPs_Assignment.dll"]
