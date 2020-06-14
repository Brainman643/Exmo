namespace Exmo.Models
{
    public class CreateStopMarketOrderRequest
    {
        /// <summary>
        /// Gets or sets the currency pair.
        /// </summary>
        public CurrencyPair Pair { get; set; }

        /// <summary>
        /// Gets or sets the quantity for the order.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price to execute the stop order.
        /// </summary>
        public decimal TriggerPrice { get; set; }

        /// <summary>
        /// Gets or sets the stop order type.
        /// </summary>
        public StopMarketOrderType Type { get; set; }
    }
}
