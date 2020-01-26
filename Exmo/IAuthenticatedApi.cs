using System.Threading;
using System.Threading.Tasks;
using Exmo.Models;

namespace Exmo
{
    public interface IAuthenticatedApi
    {
        Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);

        Task<Response> CancelOrderAsync(CancelOrderRequest request, CancellationToken cancellationToken = default);

        Task<PairDictionary<UserOpenOrder>> GetOpenOrdersAsync(CancellationToken cancellationToken = default);
    }
}
