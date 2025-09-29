# Етап збірки
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source
COPY . .

RUN dotnet restore ./OnlineStore/OnlineStore.csproj
RUN dotnet publish ./OnlineStore/OnlineStore.csproj -c Release -o /app

# Фінальний етап
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
COPY --from=build /app .

# ASP.NET Core буде слухати порт 8080
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "OnlineStore.dll"]
