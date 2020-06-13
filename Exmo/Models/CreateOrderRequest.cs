namespace Exmo.Models
{
    public class CreateOrderRequest
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
        /// Gets or sets the price for the order.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the order type.
        /// </summary>
        public CreateOrderType Type { get; set; }
    }
}
