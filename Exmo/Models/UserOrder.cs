using System;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class UserOrder
    {
        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public long? OrderId { get; set; }

        /// <summary>
        /// Gets or sets the stop order identifier.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public long? ParentOrderId { get; set; }

        /// <summary>
        /// Gets or sets the date and time the order was created.
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Gets or sets the order type.
        /// </summary>
        public OrderType Type { get; set; }

        /// <summary>
        /// Gets or sets the currency pair.
        /// </summary>
        public CurrencyPair Pair { get; set; }

        /// <summary>
        /// Gets or sets the price for the order.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public decimal? Price { get; set; }

        /// <summary>
        /// Gets or sets the price to execute the stop order.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public decimal? TriggerPrice { get; set; }

        /// <summary>
        /// Gets or sets the quantity for the order.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets the amount for the order.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
