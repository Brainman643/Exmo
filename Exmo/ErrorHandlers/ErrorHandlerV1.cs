using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace Exmo.ErrorHandlers
{
    internal class ErrorHandlerV1 : IErrorHandler
    {
        private static readonly Regex ErrorMessageRegex = new Regex(@"Error\s*(?<code>\d+)(\s*:?\s*(?<message>.*)?)?", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public void HandleResponse(JToken response)
        {
            if (response.Type == JTokenType.Object)
            {
                var jObject = (JObject)response;

                var resultToken = jObject.GetValue("result", StringComparison.OrdinalIgnoreCase);
                if (resultToken?.Type == JTokenType.Boolean && !resultToken.Value<bool>())
                {
                    var errorMessage = jObject.GetValue("error", StringComparison.OrdinalIgnoreCase)?.Value<string>();
                    var (code, message) = ParseErrorMessage(errorMessage);
                    throw new ExmoApiException(message) { Code = code };
                }
            }
        }

        private static (int? Code, string Message) ParseErrorMessage(string errorMessage)
        {
            int? code = null;
            var message = string.Empty;
            if (!string.IsNullOrEmpty(errorMessage))
            {
                var match = ErrorMessageRegex.Match(errorMessage);
                if (match.Success)
                {
                    code = int.Parse(match.Groups["code"].Value);
                    message = match.Groups["message"].Value;
                }
                else
                {
                    message = errorMessage;
                }
            }

            return (code, message);
        }
    }
}
