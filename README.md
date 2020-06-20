# <img src="PackageIcon.svg" width="48"> Exmo API Client

[![Nuget](https://img.shields.io/nuget/v/Exmo)](https://www.nuget.org/packages/Exmo/)
![Build Status](https://img.shields.io/github/workflow/status/Brainman643/Exmo/Build/master)
[![Code Coverage](https://img.shields.io/coveralls/github/Brainman643/Exmo/master)](https://coveralls.io/github/Brainman643/Exmo?branch=master)
[![License](https://img.shields.io/github/license/Brainman643/Exmo)](LICENSE)

*Reading in other languages: [English](README.md), [Русский](README.ru.md).*

## Description

This is the unofficial .NET client to access the API for Exmo exchange.

The description of API can be found on <https://exmo.me/api>

This package targets .NET Standard 2.0 ([see supporting platforms](https://docs.microsoft.com/dotnet/standard/net-standard#net-implementation-support))

## Installation

### Package Manager

Open the Package Manager Console in Visual Studio, and enter the following command:

```powershell
PM> Install-Package Exmo
```

### .NET Core CLI

Open a command shell, and enter the following command:

```shell
dotnet add package Exmo
```

## Service registration

### For ASP.NET Core project

Add the following code in the method `ConfigureServices` of the class `Startup`:

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddExmoApi();
}
```

### For console project

Add the following code for the console project:

```cs
var serviceProvider = new ServiceCollection()
    .AddExmoApi()
    .BuildServiceProvider();

var publicApi = serviceProvider.GetRequiredService<IPublicApi>();
var authenticatedApi = serviceProvider.GetRequiredService<IAuthenticatedApi>();
```

## Configuration

### Storing in code

⚠️ Storing unencrypted keys is not secure.

```cs
services.AddExmoApi(options => {
    options.PublicKey = "*****";
    options.SecretKey = "*****";
});
```

### Storing in appsettings.json file

Create the file `appsettings.json` in the project folder and add the section Exmo with PublicKey and SecretKey:

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
    .AddExmoApi()
    .Configure<ExmoOptions>(configuration.GetSection("Exmo"))
    .BuildServiceProvider();
```

### Storing in User Secrets

Use [User Secrets](https://docs.microsoft.com/aspnet/core/security/app-secrets) if you want to hide PublicKey and SecretKey.

Run the following commands to setup user secrets in the project and to add PublicKey and SecretKey:

```shell
dotnet user-secrets init
dotnet user-secrets set "Exmo:PublicKey" "*****"
dotnet user-secrets set "Exmo:SecretKey" "*****"
```

Then add the following code:

```cs
var configuration = new ConfigurationBuilder()
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .Build();

var serviceProvider = new ServiceCollection()
    .AddExmoApi()
    .Configure<ExmoOptions>(configuration.GetSection("Exmo"))
    .BuildServiceProvider();
```

## Usage

### Usage of Public API

Resolve the `IPublicApi` service to access Public API.

Getting the list of deals by currency pairs:

```cs
var pairs = new CurrencyPairCollection("BTC_USD", "ETH_USD");
var trades = await publicApi.GetTradesAsync(pairs);
```

### Usage of Athenticated API, Wallet API and Excode API

Resolve the `IAuthenticatedApi` to access Athenticated API, Wallet API and Excode API.

Getting the list of the user's open orders:

```cs
var openOrders = await authenticatedApi.GetOpenOrdersAsync();
```

## Error handling

The client methods don't return the request status. If the server cannot execute the request, the client method throws the `ExmoApiException` instead. The given exception can be thrown by any API method.

Error handling example:

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

## Logging

Create a class inherited from the class [DelegatingHandler](https://docs.microsoft.com/dotnet/api/system.net.http.delegatinghandler) to log HTTP requests. See the example [here](Sample).

## License

[MIT](LICENSE)
