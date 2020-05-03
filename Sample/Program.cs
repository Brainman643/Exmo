using System;
using System.Threading.Tasks;
using Exmo;
using Exmo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Sample
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.AddLogging(configure => configure
                .AddConfiguration(configuration.GetSection("Logging"))
                .AddConsole());
            services.AddExmoApi();

            using var serviceProvider = services.BuildServiceProvider();

            var publicApi = serviceProvider.GetRequiredService<IPublicApi>();

            var orderBook = await publicApi.GetOrderBookAsync(new OrderBookRequest { Pairs = new PairCollection("BTC_USDT") });

            Console.ReadKey();
        }
    }
}
