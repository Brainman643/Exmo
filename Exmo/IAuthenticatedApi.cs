using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exmo.Models;

namespace Exmo
{
    public interface IAuthenticatedApi
    {
        /// <summary>
        /// Получение информации об аккаунте пользователя.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserInfo> GetUserInfoAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Создание ордера.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Id ордера</returns>
        Task<long> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Отмена ордера.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CancelOrderAsync(long orderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение списока открытых ордеров пользователя.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PairDictionary<UserOpenOrder[]>> GetOpenOrdersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение сделок пользователя.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PairDictionary<OrderTrade[]>> GetUserTradesAsync(UserTradesRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение отмененных ордеров пользователя.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserCancelledOrder[]> GetCancelledOrdersAsync(PagedRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение истории сделок ордера.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OrderTrades> GetOrderTradesAsync(long orderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Подсчет в какую сумму обойдется покупка определенного количества валюты по конкретной валютной паре.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequiredAmount> CalculateRequiredAmountAsync(RequiredAmountRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение списка адресов для депозита криптовалют.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetDepositAddressesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение истории wallet.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WalletHistory> GetWalletHistoryAsync(WalletHistoryRequest request, CancellationToken cancellationToken = default);

        Task<CreateExcodeResult> CreateExcodeAsync(CreateExcodeRequest request, CancellationToken cancellationToken = default);

        Task<LoadExcodeResult> LoadExcodeAsync(string excode, CancellationToken cancellationToken = default);

        Task<long> WithdrawCryptAsync(WithdrawCryptRequest request, CancellationToken cancellationToken = default);

        Task<WithdrawTransaction> GetWithdrawTransactionAsync(long taskId, CancellationToken cancellationToken = default);
    }
}
