using System;
using Newtonsoft.Json.Linq;

namespace Exmo.ErrorHandlers
{
    internal class ErrorHandler : IErrorHandler
    {
        public void HandleResponse(JToken response)
        {
            if (response.Type == JTokenType.Object)
            {
                var jObject = (JObject)response;
                var errorToken = jObject.GetValue("error", StringComparison.OrdinalIgnoreCase);
                if (errorToken?.Type == JTokenType.Object)
                {
                    var error = (JObject)errorToken;
                    var code = error.GetValue("code", StringComparison.OrdinalIgnoreCase)?.Value<int>();
                    var message = error.GetValue("msg", StringComparison.OrdinalIgnoreCase)?.Value<string>() ?? string.Empty;

                    throw new ExmoApiException(message) { Code = code };
                }
            }
        }
    }
}
