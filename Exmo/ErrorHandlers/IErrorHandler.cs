using Newtonsoft.Json.Linq;

namespace Exmo.ErrorHandlers
{
    public interface IErrorHandler
    {
        void HandleResponse(JToken response);
    }
}
