using System;
using System.Threading.Tasks;
using Exmo;
using Exmo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sample.Logging;

namespace Sample
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            using var serviceProvider = ConfigureServices();

            var publicApi = serviceProvider.GetRequiredService<IPublicApi>();
            var orderBook = await publicApi.GetOrderBookAsync(new OrderBookRequest { Pairs = new CurrencyPairCollection("BTC_USDT") });

            Console.ReadKey();
        }

        private static ServiceProvider ConfigureServices()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();

            services.AddLogging(configure => configure
                .AddConfiguration(configuration.GetSection("Logging"))
                .AddConsole());

            services.AddExmoApi()
                .Configure<ExmoOptions>(configuration.GetSection("Exmo"));

            services.AddTransient<HttpLoggingHandler>();
            services.AddHttpClient(ExmoDefaults.HttpClientName)
                .AddHttpMessageHandler<HttpLoggingHandler>();

            return services.BuildServiceProvider();
        }
    }
}
