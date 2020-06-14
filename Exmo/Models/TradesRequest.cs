using Newtonsoft.Json;

namespace Exmo.Models
{
    internal class TradesRequest
    {
        [JsonProperty("pair")]
        public CurrencyPairCollection Pairs { get; set; }
    }
}
