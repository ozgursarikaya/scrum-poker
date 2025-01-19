# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1.1 Projelerin csproj dosyalarını kopyala ve bağımlılıkları restore et
COPY ["ScrumPoker.Web/ScrumPoker.Web.csproj", "ScrumPoker.Web/"]
COPY ["ScrumPoker.Business/ScrumPoker.Business.csproj", "ScrumPoker.Business/"]
COPY ["ScrumPoker.DataAccess/ScrumPoker.DataAccess.csproj", "ScrumPoker.DataAccess/"]
COPY ["ScrumPoker.DataProvider/ScrumPoker.DataProvider.csproj", "ScrumPoker.DataProvider/"]

# Restore işlemi için çalışma dizinini ayarla
WORKDIR /src/ScrumPoker.Web
RUN dotnet restore

# 1.2 Tüm dosyaları kopyala ve publish et
WORKDIR /src
COPY . .
WORKDIR /src/ScrumPoker.Web
RUN dotnet publish -c Release -o /app

# 2. Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Publish edilen dosyaları kopyala
COPY --from=build /app .
ENV ASPNETCORE_HTTP_PORTS=8081
# Portları aç
EXPOSE 8081

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "ScrumPoker.Web.dll"]
