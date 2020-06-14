namespace Exmo.Models
{
    public class RequiredAmountRequest
    {
        /// <summary>
        /// Gets or sets the currency pair.
        /// </summary>
        public CurrencyPair Pair { get; set; }

        /// <summary>
        /// Gets or sets the quantity to buy.
        /// </summary>
        public decimal Quantity { get; set; }
    }
}
