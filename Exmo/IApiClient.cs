using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Exmo
{
    public interface IApiClient
    {
        Task<TResponse> GetAsync<TResponse>(string apiName, object queryData, CancellationToken cancellationToken = default);

        Task<TResponse> PostAsync<TResponse>(string apiName, object postData, CancellationToken cancellationToken = default);

        Task<TResponse> SendAsync<TResponse>(string apiName, HttpMethod httpMethod, object queryData, object postData, CancellationToken cancellationToken = default);
    }
}
