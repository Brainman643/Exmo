using System.Net.Http;

namespace Exmo.Tests
{
    internal class FakeHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpMessageHandler _httpMessageHandler;

        public FakeHttpClientFactory(HttpMessageHandler httpMessageHandler)
            => _httpMessageHandler = httpMessageHandler;

        public HttpClient CreateClient(string name)
            => new HttpClient(_httpMessageHandler);
    }
}
