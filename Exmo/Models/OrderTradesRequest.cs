using Newtonsoft.Json;

namespace Exmo.Models
{
    public class OrderTradesRequest
    {
        /// <summary>
        /// Идентификатор ордера.
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }
    }
}
