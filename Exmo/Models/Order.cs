using Exmo.JsonConverters;
using Newtonsoft.Json;

namespace Exmo.Models
{
    [JsonConverter(typeof(OrderConverter))]
    public class Order
    {
        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public decimal Amount { get; set; }
    }
}
