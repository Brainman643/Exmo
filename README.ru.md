# <img src="PackageIcon.svg" width="48" style="vertical-align: middle;"> Exmo API Client

[![Nuget](https://img.shields.io/nuget/v/Exmo)](https://www.nuget.org/packages/Exmo/)
![Build Status](https://img.shields.io/github/workflow/status/Brainman643/Exmo/Build/master)
[![Code Coverage](https://img.shields.io/coveralls/github/Brainman643/Exmo/master)](https://coveralls.io/github/Brainman643/Exmo?branch=master)
[![License](https://img.shields.io/github/license/Brainman643/Exmo)](LICENSE)

*Читать на других языках: [English](README.md), [Русский](README.ru.md).*

## Описание

Это неофициальный nuget-пакет для доступа к API биржи Exmo.

Описание API можно найти сайте https://exmo.me/ru/api

netstandard2.0

## Подключение

### Для ASP.NET Core проекта

```cs
services.AddExmoApi();
```

### Для консольного проекта

```cs
var configuration = new ConfigurationBuilder()
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .Build();

var serviceProvider = new ServiceCollection()
    .Configure<ExmoOptions>(configuration)
    .AddExmoApi()
    .BuildServiceProvider();

var publicApi = serviceProvider.GetRequiredService<IPublicApi>();
var authenticatedApi = serviceProvider.GetRequiredService<IAuthenticatedApi>();
```

## Настройка

### Явно прописать в коде

### Использование appsettings.json

### Использование UserSecrets

## Примеры

### Список сделок по валютной паре

```cs
var pairs = new PairCollection("BTC_USD", "ETH_USD");
var trades = await publicApi.GetTradesAsync(pairs);
```

### Получение списка открытых ордеров пользователя

```cs
var openOrders = await authenticatedApi.GetOpenOrdersAsync();
```

## Обработка ошибок

Методы клиента не возвращают никакого статуса выполнения запроса. Вместо этого, если сервер не может выполнить запрос, то метод бросает исключение `ExmoApiException`. Данное исключение может быть брошено любым методом.

Пример:

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


## Лицензия

[MIT](LICENSE)