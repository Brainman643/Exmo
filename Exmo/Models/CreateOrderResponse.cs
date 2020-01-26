using Newtonsoft.Json;

namespace Exmo.Models
{
    public class CreateOrderResponse : Response
    {
        [JsonProperty("order_id")]
        public long OrderId { get; set; }
    }
}
