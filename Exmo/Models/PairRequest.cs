using Newtonsoft.Json;

namespace Exmo.Models
{
    public class PairRequest
    {
        [JsonProperty("pair")]
        public PairCollection Pairs { get; set; }
    }
}
