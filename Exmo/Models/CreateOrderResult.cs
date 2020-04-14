using Newtonsoft.Json;

namespace Exmo.Models
{
    internal class CreateOrderResult : Result
    {
        [JsonProperty("order_id")]
        public long OrderId { get; set; }
    }
}
