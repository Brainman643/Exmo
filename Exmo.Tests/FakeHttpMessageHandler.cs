using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Exmo.Tests
{
    internal class FakeHttpMessageHandler : HttpMessageHandler
    {
        public HttpResponseMessage ResponseMessage { get; set; }

        public Func<HttpRequestMessage, Task> HandleRequestAsync { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (HandleRequestAsync != null)
            {
                await HandleRequestAsync(request);
            }

            return ResponseMessage;
        }
    }
}
