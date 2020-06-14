using System.Threading;
using System.Threading.Tasks;
using Exmo.Models;

namespace Exmo
{
    public interface IPublicApi
    {
        /// <summary>
        /// Gets the list of the deals by currency pairs.
        /// </summary>
        /// <param name="pairs"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CurrencyPairDictionary<Trade[]>> GetTradesAsync(CurrencyPairCollection pairs, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the book of current orders by currency pairs.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CurrencyPairDictionary<OrderBook>> GetOrderBookAsync(OrderBookRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the statistics on prices and volume of trades by currency pairs.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CurrencyPairDictionary<Ticker>> GetTickerAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the currency pairs settings.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CurrencyPairDictionary<CurrencyPairSettings>> GetCurrencyPairSettingsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the list of all currencies.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string[]> GetCurrenciesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Calculates the sum of buying a certain amount of currency for the particular currency pair.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequiredAmount> CalculateRequiredAmountAsync(RequiredAmountRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the candles history.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Candle[]> GetCandlesHistoryAsync(CandlesHistoryRequest request, CancellationToken cancellationToken = default);
    }
}
