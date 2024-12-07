# Shikanoko_server
Shikanoko_server — проект, реализующий серверную часть системы изучения японских иероглифов (кандзи) Shikanoko с использованием REST API.

# Сделано с помощью
* ASP.NET Core (Minimal API)
* .NET (Версия - 8.0)
* MSTest

## Функциональность
* Получение списка всех доступных кандзи
* Получение конкретного кандзи, используя его ID
* Добавление нового кандзи
* Удаление любого кандзи
* Реализована документация REST API в Swagger UI (можно получить доступ при сборке и запуске проекта в Visual Studio по пути ".../swagger/index.html")

## Сборка, тестирование и развертывание
* Github Actions выполняет сборку проекта и его тестирование
* Переменные окружения (Secrets) настроены и проект автоматически разворачивается на личном удаленном сервере

## Сборка и запуск Docker-контейнера
1. Перейдите в папку проекта:
```console
cd Shikanoko_server
```
2. Сборка проекта:
```console
dotnet publish -c Release
```
3. Убедитесь, что в корне проекта находится файл Dockerfile:
``` Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Shikanoko_server.dll"]
```
4. Сборка Docker-образа:
```console
docker build -t shikanoko_server -f Dockerfile .
```
5. Сборка Docker-контейнера
```console
docker create --name core-shikanoko shikanoko_server
```
6. Запуск контейнера:
```console
docker start core-shikanoko
```
7. Подключение к контейнеру:
```console
docker run -it --rm -p <port>:8080 shikanoko_server
```
8. Проверка работы API:
```console
curl http://localhost:<port>/kanji
```

```console
curl http://localhost:<port>/kanji/<id>
```

### Credits
* Programmers: [Itachi aka me](https://github.com/ltachiUchiha)
