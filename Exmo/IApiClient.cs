using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Exmo
{
    public interface IApiClient
    {
        Task<TResponse> SendAsync<TResponse>(string apiName, HttpMethod httpMethod, object queryData, object postData, CancellationToken cancellationToken = default);
    }
}
