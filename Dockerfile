FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar NuGet.Config
COPY NuGet.Config ./

# Asegurarse de que el proxy esté configurado correctamente
ENV HTTP_PROXY="http://fernando.rodriguez:1:%2B%5E%3FuE7%2F6j%3C@wsaprd.syc.loc:3128"
ENV HTTPS_PROXY="http://fernando.rodriguez:1:%2B%5E%3FuE7%2F6j%3C@wsaprd.syc.loc:3128"
ENV NO_PROXY=".svc,.cluster.local,localhost,127.0.0.1,10.0.0.0/8,*.syc.loc,syc.com.co"
ENV DOTNET_CLI_HOME="/tmp"

# Verificación de variables de entorno (proxy)
RUN echo $HTTP_PROXY
RUN echo $HTTPS_PROXY

# Copiar archivos .csproj
COPY ["RedisDispatcher.API/RedisDispatcher.API.csproj", "RedisDispatcher.API/"]
COPY ["RedisDispatcher.Application/RedisDispatcher.Application.csproj", "RedisDispatcher.Application/"]
COPY ["RedisDispatcher.Domain/RedisDispatcher.Domain.csproj", "RedisDispatcher.Domain/"]
COPY ["RedisDispatcher.Infrastructure/RedisDispatcher.Infrastructure.csproj", "RedisDispatcher.Infrastructure/"]

# Verificación de acceso al archivo NuGet.Config
RUN cat /src/NuGet.Config

# Intentar restaurar los paquetes
RUN dotnet restore "RedisDispatcher.API/RedisDispatcher.API.csproj"

# Copiar el resto de los archivos
COPY . .

WORKDIR "/src/RedisDispatcher.API"
RUN dotnet publish "RedisDispatcher.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RedisDispatcher.API.dll"]
