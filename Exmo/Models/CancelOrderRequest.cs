using Newtonsoft.Json;

namespace Exmo.Models
{
    public class CancelOrderRequest
    {
        [JsonProperty("order_id")]
        public long OrderId { get; set; }
    }
}
