# SmsTestApp

Репозиторий является выполнением тестового задания, описанного в файле **Тестовое задание.pdf**.

## Стек технологий

- .NET 8
- ASP.NET Core (REST + gRPC)
- PostgreSQL 15
- Docker / Docker Compose
- WPF (Caliburn.Micro)
- Serilog

## Структура решения

| Проект | Описание |
|---|---|
| **SmsTestApp.Api** | Серверное API-приложение с поддержкой REST (HTTP/1.1, порт `8080`) и gRPC (HTTP/2, порт `50051`). Предоставляет эндпоинты для работы с меню и заказами. В Development-режиме доступен Swagger UI. |
| **SmsTestApp.ConsoleClient** | Тестовый консольный клиент для взаимодействия с API. Поддерживает подключение по HTTP и gRPC. Конфигурируется через `appsettings.json` (адреса эндпоинтов, учётные данные, строка подключения к БД). |
| **SmsTestApp.WpfClient** | Тестовый WPF-клиент на базе фреймворка Caliburn.Micro. Отображает набор переменных среды, загружаемых из конфигурации. Использует DI (`Microsoft.Extensions.DependencyInjection`) и логирование через Serilog. |
| **SmsTestApp.Contracts** | Общие контракты (DTO), используемые как сервером, так и клиентами. |

## Запуск тестового контура (Docker Compose)

Тестовый контур включает API-сервис и базу данных PostgreSQL.

### Предварительные требования

- [Docker](https://docs.docker.com/get-docker/) и Docker Compose

### Шаги запуска

1. Клонируйте репозиторий и перейдите в корневую директорию:

```git clone https://github.com/vsevolod-nikitin/SmsTestApp.git cd SmsTestApp```

2. Соберите и запустите контейнеры:

```docker-compose up -d --build```

3. После запуска будут доступны следующие сервисы:

| Сервис | Адрес |
|---|---|
| API (REST) | `http://localhost:18998` |
| API (gRPC) | `http://localhost:18999` |
| PostgreSQL | `localhost:18997` |

   Учётные данные PostgreSQL (задаются в `docker-compose.override.yml`):
   - **Пользователь:** `test_user`
   - **Пароль:** `test_password`

4. Swagger UI доступен по адресу:
http://localhost:18998/swagger

### Остановка контура

Чтобы остановить и удалить контейнеры, выполните команду:

```docker-compose down```

## Запуск клиентских приложений

### SmsTestApp.ConsoleClient

1. Убедитесь, что тестовый контур запущен (см. выше).
2. Настройте `appsettings.json` в проекте — укажите адреса эндпоинтов и строку подключения к БД.
3. Запустите проект:

```dotnet run --project SmsTestApp.ConsoleClient```

### SmsTestApp.WpfClient

1. Настройте секцию `EnvironmentVariables` в `appsettings.json` проекта.
2. Запустите проект из Visual Studio или командной строки:

```dotnet run --project SmsTestApp.WpfClient```