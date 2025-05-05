FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ENV HTTP_PROXY=http://fernando.rodriguez:1:%2B%5E%3FuE7%2F6j%3C@wsaprd.syc.loc:3128
ENV HTTPS_PROXY=http://fernando.rodriguez:1:%2B%5E%3FuE7%2F6j%3C@wsaprd.syc.loc:3128
ENV NO_PROXY=localhost,127.0.0.1,.svc,.cluster.local,10.0.0.0/8

COPY ["RedisDispatcher.API/RedisDispatcher.API.csproj", "RedisDispatcher.API/"]
COPY ["RedisDispatcher.Application/RedisDispatcher.Application.csproj", "RedisDispatcher.Application/"]
COPY ["RedisDispatcher.Domain/RedisDispatcher.Domain.csproj", "RedisDispatcher.Domain/"]
COPY ["RedisDispatcher.Infrastructure/RedisDispatcher.Infrastructure.csproj", "RedisDispatcher.Infrastructure/"]

RUN dotnet restore "RedisDispatcher.API/RedisDispatcher.API.csproj"

COPY . .
WORKDIR "/src/RedisDispatcher.API"
RUN dotnet publish "RedisDispatcher.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RedisDispatcher.API.dll"]
