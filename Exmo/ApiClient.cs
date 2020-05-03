using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Exmo.ErrorHandlers;
using Exmo.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Exmo
{
    public class ApiClient : IApiClient
    {
        private readonly Uri _baseAddress;
        private readonly HttpClient _httpClient;
        private readonly IList<IErrorHandler> _errorHandlers = new List<IErrorHandler>
        {
            new ErrorHandlerV1(),
            new ErrorHandler()
        };

        protected ILogger<ApiClient> Logger { get; }

        public ApiClient(IOptions<ExmoOptions> options, HttpClient httpClient, ILogger<ApiClient> logger)
        {
            _baseAddress = new Uri(AddTrailingSlash(options.Value.ExmoApiUrl));
            _httpClient = httpClient;
            _httpClient.BaseAddress = _baseAddress;
            Logger = logger;
        }

        public async Task<TResponse> SendAsync<TResponse>(string apiName, HttpMethod httpMethod, object queryData, object postData, CancellationToken cancellationToken = default)
        {
            var query = JsonToFormUrlEncodedConverter.Convert(queryData);
            var data = JsonToFormUrlEncodedConverter.Convert(postData);
            var response = await SendAsync(apiName, httpMethod, query, data, cancellationToken);
            var jsonToken = JsonHelper.Serializer.Deserialize<JToken>(response);

            //try handling an error response
            foreach (var errorHandler in _errorHandlers)
            {
                errorHandler.HandleResponse(jsonToken);
            }

            return jsonToken.ToObject<TResponse>(JsonHelper.Serializer);
        }

        protected async Task<string> SendAsync(string apiName, HttpMethod httpMethod, List<KeyValuePair<string, string>> queryData, List<KeyValuePair<string, string>> postData, CancellationToken cancellationToken = default)
        {
            var address = apiName;

            var queryString = await GetQueryStringAsync(queryData);
            if (!string.IsNullOrEmpty(queryString))
            {
                address += "?" + queryString;
            }

            var request = new HttpRequestMessage(httpMethod, address);
            request.Content = await GetContentAsync(postData);

            using (var response = await _httpClient.SendAsync(request, cancellationToken))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ExmoApiException($"Exmo server returned a status code {(int)response.StatusCode}.");
                }

                using (var content = response.Content)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        protected virtual Task<FormUrlEncodedContent> GetContentAsync(List<KeyValuePair<string, string>> data)
        {
            return Task.FromResult(new FormUrlEncodedContent(data));
        }

        private static async Task<string> GetQueryStringAsync(List<KeyValuePair<string, string>> data)
        {
            var content = new FormUrlEncodedContent(data);
            return await content.ReadAsStringAsync();
        }

        private static string AddTrailingSlash(string address)
        {
            return address.EndsWith("/") ? address : address + "/";
        }
    }
}
