using System.Threading;
using System.Threading.Tasks;
using Exmo.Models;

namespace Exmo
{
    public class PublicApi : IPublicApi
    {
        private readonly IApiClient _client;

        public PublicApi(IApiClient client)
        {
            _client = client;
        }

        public Task<PairDictionary<Trade[]>> GetTradesAsync(PairCollection pairs, CancellationToken cancellationToken = default)
        {
            var request = new TradesRequest { Pairs = pairs };
            return _client.GetAsync<PairDictionary<Trade[]>>("trades", request, cancellationToken);
        }

        public Task<PairDictionary<OrderBook>> GetOrderBookAsync(OrderBookRequest request, CancellationToken cancellationToken = default)
        {
            return _client.GetAsync<PairDictionary<OrderBook>>("order_book", request, cancellationToken);
        }

        public Task<PairDictionary<Ticker>> GetTickerAsync(CancellationToken cancellationToken = default)
        {
            return _client.GetAsync<PairDictionary<Ticker>>("ticker", null, cancellationToken);
        }

        public Task<PairDictionary<PairSettings>> GetPairSettingsAsync(CancellationToken cancellationToken = default)
        {
            return _client.GetAsync<PairDictionary<PairSettings>>("pair_settings", null, cancellationToken);
        }

        public Task<string[]> GetCurrenciesAsync(CancellationToken cancellationToken = default)
        {
            return _client.GetAsync<string[]>("currency", null, cancellationToken);
        }
    }
}
