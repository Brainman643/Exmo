using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Xunit;

namespace Exmo.Tests
{
    public sealed class ApiClientTests : IDisposable
    {
        private class FakeResponse
        {
            public decimal DecimalProperty { get; set; }
            public string StringProperty { get; set; }
        }

        private readonly FakeHttpMessageHandler _fakeHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly IApiClient _apiClient;

        public ApiClientTests()
        {
            _fakeHttpMessageHandler = new FakeHttpMessageHandler();
            _httpClient = new HttpClient(_fakeHttpMessageHandler, true);
            _apiClient = new ApiClient(Options.Create(new ExmoOptions()), _httpClient, new NullLogger<ApiClient>());
        }

        [Fact]
        public async Task Send_ResultIsFalse_ThrowsExmoApiException()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"result\": false, \"error\": \"Test Error\"}")
            };

            await Assert.ThrowsAsync<ExmoApiException>(async () => await _apiClient.SendAsync<object>("test", HttpMethod.Get, null, null));
        }

        [Fact]
        public async Task Send_NotAllPropertiesAreSet_ThrowsJsonSerializationException()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"decimalProperty\": 1}")
            };

            await Assert.ThrowsAsync<JsonSerializationException>(async () => await _apiClient.SendAsync<FakeResponse>("test", HttpMethod.Get, null, null));
        }

        [Fact]
        public async Task Send_StatusCodeIsNotSuccess_ThrowsExmoApiException()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

            await Assert.ThrowsAsync<ExmoApiException>(async () => await _apiClient.SendAsync<FakeResponse>("test", HttpMethod.Get, null, null));
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
