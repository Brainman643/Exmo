using System;
using Newtonsoft.Json.Linq;

namespace Exmo.ErrorHandlers
{
    internal class CandleHistoryErrorHandler : IErrorHandler
    {
        public void HandleResponse(JToken response)
        {
            if (response.Type == JTokenType.Object)
            {
                var jObject = (JObject)response;
                var errorToken = jObject.GetValue("s", StringComparison.OrdinalIgnoreCase);
                if (errorToken?.Type == JTokenType.String && string.Equals(errorToken.Value<string>(), "error", StringComparison.OrdinalIgnoreCase))
                {
                    var message = jObject.GetValue("errmsg", StringComparison.OrdinalIgnoreCase)?.Value<string>() ?? string.Empty;
                    throw new ExmoApiException(message);
                }
            }
        }
    }
}
