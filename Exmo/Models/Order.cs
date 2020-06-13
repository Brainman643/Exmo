using Exmo.Json.Converters;
using Newtonsoft.Json;

namespace Exmo.Models
{
    [JsonConverter(typeof(OrderConverter))]
    public class Order
    {
        /// <summary>
        /// Gets or sets the price for the order.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity for the order (the amount in the base currency).
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets the amount for the order (the amount in the quote currency).
        /// </summary>
        public decimal Amount { get; set; }
    }
}
