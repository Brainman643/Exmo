using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exmo.Models;

namespace Exmo
{
    public class AuthenticatedApi : IAuthenticatedApi
    {
        private readonly IApiClient _client;

        public AuthenticatedApi(IAuthApiClient client)
        {
            _client = client;
        }

        public Task<UserInfo> GetUserInfoAsync(CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<UserInfo>("user_info", null, cancellationToken);
        }

        public async Task<long> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _client.PostAsync<CreateOrderResult>("order_create", request, cancellationToken);
            return result.OrderId;
        }

        public async Task CancelOrderAsync(long orderId, CancellationToken cancellationToken = default)
        {
            var request = new CancelOrderRequest
            {
                OrderId = orderId
            };
            await _client.PostAsync<Result>("order_cancel", request, cancellationToken);
        }

        public Task<PairDictionary<UserOpenOrder[]>> GetOpenOrdersAsync(CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<PairDictionary<UserOpenOrder[]>>("user_open_orders", null, cancellationToken);
        }

        public Task<PairDictionary<OrderTrade[]>> GetUserTradesAsync(UserTradesRequest request, CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<PairDictionary<OrderTrade[]>>("user_trades", request, cancellationToken);
        }

        public Task<UserCancelledOrder[]> GetCancelledOrdersAsync(PagedRequest request, CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<UserCancelledOrder[]>("user_cancelled_orders", request, cancellationToken);
        }

        public Task<OrderTrades> GetOrderTradesAsync(long orderId, CancellationToken cancellationToken = default)
        {
            var request = new OrderTradesRequest
            {
                OrderId = orderId
            };
            return _client.PostAsync<OrderTrades>("order_trades", request, cancellationToken);
        }

        public Task<RequiredAmount> CalculateRequiredAmountAsync(RequiredAmountRequest request, CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<RequiredAmount>("required_amount", request, cancellationToken);
        }

        public Task<Dictionary<string, string>> GetDepositAddressesAsync(CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<Dictionary<string, string>>("deposit_address", cancellationToken);
        }

        public Task<WalletHistory> GetWalletHistoryAsync(WalletHistoryRequest request, CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<WalletHistory>("wallet_history", request, cancellationToken);
        }

        public Task<CreateExcodeResult> CreateExcodeAsync(CreateExcodeRequest request, CancellationToken cancellationToken = default)
        {
            return _client.PostAsync<CreateExcodeResult>("excode_create", request, cancellationToken);
        }

        public Task<LoadExcodeResult> LoadExcodeAsync(string excode, CancellationToken cancellationToken = default)
        {
            var request = new LoadExcodeRequest { Code = excode };
            return _client.PostAsync<LoadExcodeResult>("excode_load", request, cancellationToken);
        }

        public async Task<long> WithdrawCryptAsync(WithdrawCryptRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _client.PostAsync<WithdrawCryptResult>("withdraw_crypt", request, cancellationToken);
            return result.TaskId;
        }

        public Task<WithdrawTransaction> GetWithdrawTransactionAsync(long taskId, CancellationToken cancellationToken = default)
        {
            var request = new WithdrawTransactionRequest
            {
                TaskId = taskId
            };
            return _client.PostAsync<WithdrawTransaction>("withdraw_get_txid", request, cancellationToken);
        }
    }
}
