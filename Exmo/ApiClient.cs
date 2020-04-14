using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        protected ILogger<ApiClient> Logger { get; }

        public ApiClient(IOptions<ExmoOptions> options, HttpClient httpClient, ILogger<ApiClient> logger)
        {
            _baseAddress = new Uri(AddTrailingSlash(options.Value.ExmoApiUrl));
            _httpClient = httpClient;
            _httpClient.BaseAddress = _baseAddress;
            Logger = logger;
        }

        public Task<TResponse> GetAsync<TResponse>(string apiName, object queryData, CancellationToken cancellationToken = default)
        {
            return SendAsync<TResponse>(apiName, HttpMethod.Get, queryData, null, cancellationToken);
        }

        public Task<TResponse> PostAsync<TResponse>(string apiName, object postData, CancellationToken cancellationToken = default)
        {
            return SendAsync<TResponse>(apiName, HttpMethod.Post, null, postData, cancellationToken);
        }

        public async Task<TResponse> SendAsync<TResponse>(string apiName, HttpMethod httpMethod, object queryData, object postData, CancellationToken cancellationToken = default)
        {
            var query = JsonToFormUrlEncodedConverter.Convert(queryData);
            var data = JsonToFormUrlEncodedConverter.Convert(postData);
            var responseStr = await SendAsync(apiName, httpMethod, query, data, cancellationToken);

            //try handling an error response
            var jsonToken = JsonHelper.Serializer.Deserialize<JToken>(responseStr);
            if (jsonToken.Type == JTokenType.Object)
            {
                var response = jsonToken.ToObject<ResultResponse>(JsonHelper.Serializer);
                if (!response.Succeeded)
                {
                    throw new ExmoApiException(response.Error);
                }
            }

            return jsonToken.ToObject<TResponse>(JsonHelper.Serializer);
        }

        private async Task<string> SendAsync(string apiName, HttpMethod httpMethod, List<KeyValuePair<string, string>> queryData, List<KeyValuePair<string, string>> postData, CancellationToken cancellationToken = default)
        {
            var address = apiName;

            var queryString = await GetQueryStringAsync(queryData);
            if (!string.IsNullOrEmpty(queryString))
            {
                address += "?" + queryString;
            }

            var logTitle = $"{httpMethod}: {_baseAddress}{queryString}";
            using (Logger.BeginScope(logTitle))
            {
                var requestMessage = new HttpRequestMessage(httpMethod, address);
                requestMessage.Content = await GetContentAsync(postData);

                if (Logger.IsEnabled(LogLevel.Trace))
                {
                    var requestText = await requestMessage.Content.ReadAsStringAsync();
                    var sb = new StringBuilder();
                    sb.AppendLine(logTitle);
                    if (!string.IsNullOrEmpty(requestText))
                    {
                        sb.Append("Request: ").Append(requestText);
                    }

                    Logger.LogTrace(sb.ToString());
                }

                Logger.LogInformation(logTitle);
                using (var response = await _httpClient.SendAsync(requestMessage, cancellationToken))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new ExmoApiException($"Exmo server returned a status code {(int)response.StatusCode}.");
                    }

                    using (var content = response.Content)
                    {
                        var responseText = await response.Content.ReadAsStringAsync();

                        if (Logger.IsEnabled(LogLevel.Trace))
                        {
                            var sb = new StringBuilder();
                            sb.AppendLine(logTitle);
                            if (!string.IsNullOrEmpty(responseText))
                            {
                                sb.Append("Response: ").Append(responseText);
                            }

                            Logger.LogTrace(sb.ToString());
                        }

                        return responseText;
                    }
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
