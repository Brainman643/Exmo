using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Exmo.Models
{
    public class OrderTrade : Trade
    {
        /// <summary>
        /// Идентификатор ордера.
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// Валютная пара.
        /// </summary>
        public Pair Pair { get; set; }
    }
}
