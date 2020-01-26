using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Exmo
{
    public class ApiClient : IApiClient
    {
        private const string Url = "http://api.exmo.com/v1/{0}/";

        public async Task<TResponse> SendAsync<TResponse>(string apiName, HttpMethod httpMethod, object queryData, object postData, CancellationToken cancellationToken = default)
        {
            var query = queryData != null ? JsonToFormUrlEncodedConverter.Convert(queryData) : null;
            var data = postData != null ? JsonToFormUrlEncodedConverter.Convert(postData) : null;
            var responseStr = await SendAsync(apiName, httpMethod, query, data, cancellationToken);
            return JsonHelper.Serializer.Deserialize<TResponse>(responseStr);
        }

        private async Task<string> SendAsync(string apiName, HttpMethod httpMethod, IList<KeyValuePair<string, string>> queryData, IList<KeyValuePair<string, string>> postData, CancellationToken cancellationToken = default)
        {
            using (var client = new HttpClient())
            {
                var address = string.Format(Url, apiName);
                if (queryData != null)
                {
                    address += "?" + await GetQueryStringAsync(queryData);
                }

                var requestMessage = new HttpRequestMessage(httpMethod, address);
                if (postData != null)
                {
                    requestMessage.Content = await GetContentAsync(postData);
                }

                using (var response = await client.SendAsync(requestMessage, cancellationToken))
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        protected virtual Task<FormUrlEncodedContent> GetContentAsync(IList<KeyValuePair<string, string>> data)
        {
            return Task.FromResult(new FormUrlEncodedContent(data));
        }

        private static async Task<string> GetQueryStringAsync(IList<KeyValuePair<string, string>> data)
        {
            var content = new FormUrlEncodedContent(data);
            return await content.ReadAsStringAsync();
        }
    }
}
