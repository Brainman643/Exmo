using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Exmo.Models;

namespace Exmo
{
    public class PublicApi : IPublicApi
    {
        private readonly IApiClient _client = new ApiClient();

        public Task<PairDictionary<Trade[]>> GetTradesAsync(PairCollection pairs, CancellationToken cancellationToken = default)
        {
            if (pairs == null)
            {
                throw new ArgumentNullException(nameof(pairs));
            }
            if (pairs.Count == 0)
            {
                throw new ArgumentException("Pairs must contain at least one pair.", nameof(pairs));
            }
            var request = new PairRequest { Pairs = pairs };
            return _client.SendAsync<PairDictionary<Trade[]>>("trades", HttpMethod.Get, request, null, cancellationToken);
        }

        public Task<PairDictionary<OrderBook>> GetOrderBookAsync(PairCollection pairs, CancellationToken cancellationToken = default)
        {
            if (pairs == null)
            {
                throw new ArgumentNullException(nameof(pairs));
            }
            if (pairs.Count == 0)
            {
                throw new ArgumentException("Pairs must contain at least one pair.", nameof(pairs));
            }

            var request = new PairRequest {Pairs = pairs};
            return _client.SendAsync<PairDictionary<OrderBook>>("order_book", HttpMethod.Get, request, null, cancellationToken);
        }

        public Task<PairDictionary<Ticker>> GetTickerAsync(CancellationToken cancellationToken = default)
        {
            return _client.SendAsync<PairDictionary<Ticker>>("ticker", HttpMethod.Get, null, null, cancellationToken);
        }

        public Task<PairDictionary<PairSettings>> GetPairSettingsAsync(CancellationToken cancellationToken = default)
        {
            return _client.SendAsync<PairDictionary<PairSettings>>("pair_settings", HttpMethod.Get, null, null, cancellationToken);
        }

        public Task<string[]> GetCurrenciesAsync(CancellationToken cancellationToken = default)
        {
            return _client.SendAsync<string[]>("currency", HttpMethod.Get, null, null, cancellationToken);
        }
    }
}
