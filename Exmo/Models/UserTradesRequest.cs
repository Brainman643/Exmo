using Newtonsoft.Json;

namespace Exmo.Models
{
    public class UserTradesRequest : PagedRequest
    {
        [JsonProperty("pair")]
        public CurrencyPairCollection Pairs { get; set; }
    }
}
