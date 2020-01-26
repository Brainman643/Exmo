using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Exmo.Models;

namespace Exmo
{
    public class AuthenticatedApi : IAuthenticatedApi
    {
        private readonly IApiClient _client;

        public AuthenticatedApi(ExmoOptions configuration)
        {
            _client = new AuthApiClient(configuration);
        }

        public AuthenticatedApi(string publicKey, string secretKey)
        {
            _client = new AuthApiClient(publicKey, secretKey);
        }

        public Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
        {
            return _client.SendAsync<CreateOrderResponse>("order_create", HttpMethod.Post, null, request, cancellationToken);
        }

        public Task<Response> CancelOrderAsync(CancelOrderRequest request, CancellationToken cancellationToken = default)
        {
            return _client.SendAsync<Response>("order_cancel", HttpMethod.Post, null, request, cancellationToken);
        }

        public Task<PairDictionary<UserOpenOrder>> GetOpenOrdersAsync(CancellationToken cancellationToken = default)
        {
            return _client.SendAsync<PairDictionary<UserOpenOrder>>("user_open_orders", HttpMethod.Post, null, null, cancellationToken);
        }
    }
}
