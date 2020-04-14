using Newtonsoft.Json;

namespace Exmo.Models
{
    internal class TradesRequest
    {
        [JsonProperty("pair")]
        public PairCollection Pairs { get; set; }
    }
}
