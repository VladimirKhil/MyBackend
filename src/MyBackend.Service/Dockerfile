FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MyBackend.Service.Contract/MyBackend.Service.Contract.csproj", "src/MyBackend.Service.Contract/"]
COPY ["src/MyBackend.Database/MyBackend.Database.csproj", "src/MyBackend.Database/"]
COPY ["src/MyBackend.Service/MyBackend.Service.csproj", "src/MyBackend.Service/"]
RUN dotnet restore "./src/MyBackend.Service/MyBackend.Service.csproj" -r linux-musl-x64
COPY . .
WORKDIR "/src/src/MyBackend.Service"
RUN dotnet publish "./MyBackend.Service.csproj" -p:PublishSingleFile=true -r linux-musl-x64 -p:PublishTrimmed=true -p:TrimMode=partial -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["./MyBackend.Service", "--urls", "http://*:5000"]