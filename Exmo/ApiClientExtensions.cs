using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Exmo
{
    public static class ApiClientExtensions
    {
        public static Task<TResponse> GetAsync<TResponse>(this IApiClient apiClient, string apiName, object queryData, CancellationToken cancellationToken = default)
        {
            return apiClient.SendAsync<TResponse>(apiName, HttpMethod.Get, queryData, null, cancellationToken);
        }

        public static Task<TResponse> PostAsync<TResponse>(this IApiClient apiClient, string apiName, object postData, CancellationToken cancellationToken = default)
        {
            return apiClient.SendAsync<TResponse>(apiName, HttpMethod.Post, null, postData, cancellationToken);
        }
    }
}
