# <img src="PackageIcon.svg" width="48"> Exmo API Client

[![Nuget](https://img.shields.io/nuget/v/Exmo)](https://www.nuget.org/packages/Exmo/)
![Build Status](https://img.shields.io/github/workflow/status/Brainman643/Exmo/Build/master)
[![Code Coverage](https://img.shields.io/coveralls/github/Brainman643/Exmo/master)](https://coveralls.io/github/Brainman643/Exmo?branch=master)
[![License](https://img.shields.io/github/license/Brainman643/Exmo)](LICENSE)

*Читать на других языках: [English](README.md), [Русский](README.ru.md).*

## Описание

Это неофициальный .NET клиент для доступа к API биржи Exmo.

Описание API можно найти сайте <https://exmo.me/ru/api>

Этот пакет предназначен для .NET Standard 2.0 ([поддерживаемые платформы](https://docs.microsoft.com/ru-ru/dotnet/standard/net-standard#net-implementation-support))

## Установка

### Package Manager

Откройте Package Manager Console в Visual Studio и введите следующую команду:

```powershell
PM> Install-Package Exmo
```

### .NET Core CLI

Откройте командную строку и введите следующую команду:

```shell
dotnet add package Exmo
```

## Регистрация

### Для ASP.NET Core проекта

В классе `Startup` в методе `ConfigureServices` добавьте следующий код:

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddExmoApi();
}
```

### Для консольного проекта

```cs
var serviceProvider = new ServiceCollection()
    .AddExmoApi()
    .BuildServiceProvider();

var publicApi = serviceProvider.GetRequiredService<IPublicApi>();
var authenticatedApi = serviceProvider.GetRequiredService<IAuthenticatedApi>();
```

## Настройка

### Хранение прямо в коде

⚠️ Хранение ключей в открытом виде небезопасно.

```cs
services.AddExmoApi(options => {
    options.PublicKey = "*****";
    options.SecretKey = "*****";
});
```

### Хранение в файле appsettings.json

Необходимо создать в папке проекта файл `appsettings.json`, и добавить в него секцию Exmo, в которой указать PublicKey и SecretKey:

```json
{
  "Exmo": {
    "PublicKey": "*****",
    "SecretKey": "*****"
  }
}
```

```cs
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var serviceProvider = new ServiceCollection()
    .Configure<ExmoOptions>(configuration.GetSection("Exmo"))
    .AddExmoApi()
    .BuildServiceProvider();
```

### Хранение в User Secrets

Для того, чтобы скрыть свои PublicKey и SecretKey можно воспользоваться [User Secrets](https://docs.microsoft.com/ru-ru/aspnet/core/security/app-secrets).

Выполните следующие команды, чтобы добавить в проект user secrets и добавить открытый и закрытый ключи:

```shell
dotnet user-secrets init
dotnet user-secrets set "Exmo:PublicKey" "*****"
dotnet user-secrets set "Exmo:SecretKey" "*****"
```

Затем добавьте следующий код:

```cs
var configuration = new ConfigurationBuilder()
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .Build();

var serviceProvider = new ServiceCollection()
    .Configure<ExmoOptions>(configuration.GetSection("Exmo"))
    .AddExmoApi()
    .BuildServiceProvider();
```

## Примеры

### Использование Public API

Извлеките из контейнера сервис `IPublicApi` для доступа к Public API.

Получение списка сделок по валютной паре:

```cs
var pairs = new CurrencyPairCollection("BTC_USD", "ETH_USD");
var trades = await publicApi.GetTradesAsync(pairs);
```

### Использование Athenticated API, Wallet API и Excode API

Извлеките из контейнера сервис `IAuthenticatedApi` для доступа к Athenticated API, Wallet API и Excode API.

Получение списка открытых ордеров пользователя:

```cs
var openOrders = await authenticatedApi.GetOpenOrdersAsync();
```

## Обработка ошибок

Методы клиента не возвращают статус выполнения запроса. Вместо этого, если сервер не может выполнить запрос, то метод бросает исключение `ExmoApiException`. Данное исключение может быть брошено любым методом API.

Пример обработки ошибок:

```cs
try
{
    var request = new CreateOrderRequest
    {
        Pair = "BTC_USD",
        Type = OrderType.Buy,
        Quantity = 0.01m,
        Price = 9700m
    };
    var orderId = await authenticatedApi.CreateOrderAsync(request);
}
catch (ExmoApiException ex)
{
    Console.WriteLine(ex.Message);
}
```

## Логирование

Для логирования HTTP запросов вы можете создать класс, производный от класса [DelegatingHandler](https://docs.microsoft.com/ru-ru/dotnet/api/system.net.http.delegatinghandler). Пример использования можете посмотреть [здесь](Sample).

## Лицензия

[MIT](LICENSE)
