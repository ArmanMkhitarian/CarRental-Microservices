# Используем официальный образ .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

# Строим приложение
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем .csproj
COPY CarBookingService.csproj ./CarBookingService/
RUN dotnet restore "./CarBookingService/CarBookingService.csproj"

# Копируем весь исходный код
COPY . ./CarBookingService/
WORKDIR "/src/CarBookingService"
RUN dotnet publish -c Release -o /app/publish

# Запускаем приложение
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CarBookingService.dll"]
