using System.Threading;
using System.Threading.Tasks;
using Exmo.Models;

namespace Exmo
{
    public interface IPublicApi
    {
        /// <summary>
        /// Список сделок по валютной паре.
        /// </summary>
        /// <param name="pairs"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PairDictionary<Trade[]>> GetTradesAsync(PairCollection pairs,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Книга ордеров по валютной паре.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PairDictionary<OrderBook>> GetOrderBookAsync(OrderBookRequest request,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Cтатистика цен и объемов торгов по валютным парам.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PairDictionary<Ticker>> GetTickerAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Настройки валютных пар.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PairDictionary<PairSettings>> GetPairSettingsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Cписок валют биржи.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string[]> GetCurrenciesAsync(CancellationToken cancellationToken = default);
    }
}
