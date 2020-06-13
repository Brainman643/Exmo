using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Sample.Logging
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        private readonly ILogger<HttpLoggingHandler> _logger;

        public HttpLoggingHandler(ILogger<HttpLoggingHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestId = Guid.NewGuid();
            using (_logger.BeginScope("({RequestId})", requestId))
            {
                if (_logger.IsEnabled(LogLevel.Trace))
                {
                    var requestContent = await request.Content.ReadAsStringAsync();
                    _logger.LogTrace(requestContent);
                }

                var response = await base.SendAsync(request, cancellationToken);

                if (_logger.IsEnabled(LogLevel.Trace))
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogTrace(responseContent);
                }

                return response;
            }
        }
    }
}
