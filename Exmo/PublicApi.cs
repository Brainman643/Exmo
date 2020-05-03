using System.Threading;
using System.Threading.Tasks;
using Exmo.ErrorHandlers;
using Exmo.Json;
using Exmo.Models;
using Newtonsoft.Json.Linq;

namespace Exmo
{
    public class PublicApi : IPublicApi
    {
        private readonly IErrorHandler _candleHistoryErrorHandler = new CandleHistoryErrorHandler();
        private readonly IApiClient _client;

        public PublicApi(IApiClient client)
        {
            _client = client;
        }

        public Task<PairDictionary<Trade[]>> GetTradesAsync(PairCollection pairs, CancellationToken cancellationToken = default)
        {
            var request = new TradesRequest { Pairs = pairs };
            return _client.PostAsync<PairDictionary<Trade[]>>("trades", request, cancellationToken);
        }

        public Task<PairDictionary<OrderBook>> GetOrderBookAsync(OrderBookRequest request, CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<PairDictionary<OrderBook>>("order_book", request, cancellationToken);
        }

        public Task<PairDictionary<Ticker>> GetTickerAsync(CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<PairDictionary<Ticker>>("ticker", null, cancellationToken);
        }

        public Task<PairDictionary<PairSettings>> GetPairSettingsAsync(CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<PairDictionary<PairSettings>>("pair_settings", null, cancellationToken);
        }

        public Task<string[]> GetCurrenciesAsync(CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<string[]>("currency", null, cancellationToken);
        }

        public Task<RequiredAmount> CalculateRequiredAmountAsync(RequiredAmountRequest request, CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<RequiredAmount>("required_amount", request, cancellationToken);
        }

        public async Task<Candle[]> GetCandlesHistoryAsync(CandlesHistoryRequest request, CancellationToken cancellationToken = default)
        {
            var jsonToken = await _client.GetAsync<JToken>("candles_history", request, cancellationToken);
            _candleHistoryErrorHandler.HandleResponse(jsonToken);
            var result =  jsonToken.ToObject<CandlesHistory>(JsonHelper.Serializer);
            return result.Candles;
        }
    }
}
