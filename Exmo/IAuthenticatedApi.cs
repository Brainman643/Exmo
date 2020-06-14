using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exmo.Models;

namespace Exmo
{
    public interface IAuthenticatedApi
    {
        /// <summary>
        /// Gets information about the user's account.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserInfo> GetUserInfoAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates the order.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The order identifier.</returns>
        Task<long> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancels the order.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CancelOrderAsync(long orderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates the stop market order.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The stop market order identifier.</returns>
        Task<long> CreateStopMarketOrderAsync(CreateStopMarketOrderRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Cancels the stop market order.
        /// </summary>
        /// <param name="parentOrderId">The stop market order identifier.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CancelStopMarketOrderAsync(long parentOrderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the list of the user’s active orders.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CurrencyPairDictionary<UserOrder[]>> GetOpenOrdersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the list of the user’s deals.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CurrencyPairDictionary<OrderTrade[]>> GetUserTradesAsync(UserTradesRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the list of the user’s cancelled orders.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserOrder[]> GetCancelledOrdersAsync(PagedRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the history of deals with the order.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OrderTrades> GetOrderTradesAsync(long orderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the list of the addresses for the cryptocurrency deposit.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetDepositAddressesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates the EXCODE coupon.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CreateExcodeResult> CreateExcodeAsync(CreateExcodeRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads the EXCODE coupon.
        /// </summary>
        /// <param name="excode">Code of the EXCODE coupon.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<LoadExcodeResult> LoadExcodeAsync(string excode, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates the task for cryptocurrency withdrawal.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> WithdrawCryptAsync(WithdrawCryptRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the transaction ID in order to keep track of it on blockchain.
        /// </summary>
        /// <param name="taskId">Withdrawal task identifier.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WithdrawTransaction> GetWithdrawTransactionAsync(long taskId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the history of the wallet.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WalletHistory> GetWalletHistoryAsync(WalletHistoryRequest request, CancellationToken cancellationToken = default);
    }
}
