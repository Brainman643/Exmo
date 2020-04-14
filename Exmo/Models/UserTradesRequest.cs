using Newtonsoft.Json;

namespace Exmo.Models
{
    public class UserTradesRequest : PagedRequest
    {
        [JsonProperty("pair")]
        public PairCollection Pairs { get; set; }
    }
}
