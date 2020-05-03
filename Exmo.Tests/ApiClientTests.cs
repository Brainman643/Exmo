using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Exmo.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        private class FakeResponse2
        {
            [JsonProperty("exec_type", Required = Required.Default)]
            public ExecutionType? ExecutionType { get; set; }

            [JsonProperty(Required = Required.Default)]
            public decimal? CommissionAmount { get; set; }

            [JsonProperty(Required = Required.Default)]
            public string CommissionCurrency { get; set; }

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
        public async Task Send_AdditionalPropertiesInResponse()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"decimal_property\": 1,\"string_property\": \"baz\",\"other_property\": \"test\"}")
            };

            var result = await _apiClient.SendAsync<FakeResponse>("test", HttpMethod.Get, null, null);
            Assert.Equal(1, result.DecimalProperty);
            Assert.Equal("baz", result.StringProperty);
        }

        [Fact]
        public async Task Send_NotAllPropertiesAreSet_ThrowsJsonSerializationException()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"decimal_property\": 1}")
            };

            await Assert.ThrowsAsync<JsonSerializationException>(async () => await _apiClient.SendAsync<FakeResponse>("test", HttpMethod.Get, null, null));
        }

        [Fact]
        public async Task Send_StatusCodeIsNotSuccess_ThrowsExmoApiException()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);

            await Assert.ThrowsAsync<ExmoApiException>(async () => await _apiClient.SendAsync<FakeResponse>("test", HttpMethod.Get, null, null));
        }

        [Theory]
        [InlineData("{\"result\": false, \"error\": \"Error 1234: Test Error\"}", 1234, "Test Error")]
        [InlineData("{\"result\":false,\"error\":\"Error 1234\"}", 1234, "")]
        [InlineData("{\"Result\": false, \"Error\": \"Error 1234: Test Error\"}", 1234, "Test Error")]
        [InlineData("{\"error\":{\"code\":1234,\"msg\":\"Test Error\"}}", 1234, "Test Error")]
        [InlineData("{\"error\":{\"code\":1234}}", 1234, "")]
        [InlineData("{\"Error\":{\"Code\":1234,\"Msg\":\"Test Error\"}}", 1234, "Test Error")]
        [InlineData("{\"error\":{}}", -1, "")]
        public async Task Send_ErrorResponse_ThrowsExmoApiException(string response, int code, string message)
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(response)
            };

            var exception = await Assert.ThrowsAsync<ExmoApiException>(async () => await _apiClient.SendAsync<object>("test", HttpMethod.Get, null, null));
            Assert.Equal(code, exception.Code);
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public async Task Send_EmptyStringsHandling()
        {
            _fakeHttpMessageHandler.ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"exec_type\": \"\",\"commission_amount\": \"\",\"commission_currency\": \"\"}")
            };

            var fakeResponse = await _apiClient.SendAsync<FakeResponse2>("test", HttpMethod.Get, null, null);
            Assert.Null(fakeResponse.ExecutionType);
            Assert.Null(fakeResponse.CommissionAmount);
            Assert.Empty(fakeResponse.CommissionCurrency);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
