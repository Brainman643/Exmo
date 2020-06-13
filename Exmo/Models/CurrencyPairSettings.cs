namespace Exmo.Models
{
    public class CurrencyPairSettings
    {
        /// <summary>
        /// Gets or sets the minimum quantity for the order.
        /// </summary>
        public decimal MinQuantity { get; set; }

        /// <summary>
        /// Gets or sets the maximum quantity for the order.
        /// </summary>
        public decimal MaxQuantity { get; set; }

        /// <summary>
        /// Gets or sets the minimum price for the order.
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// Gets or sets the maximum price for the order.
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// Gets or sets the minimum amount for the order.
        /// </summary>
        public decimal MinAmount { get; set; }

        /// <summary>
        /// Gets or sets the maximum amount for the order.
        /// </summary>
        public decimal MaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the number of decimal places that can be used in the price when placing an order.
        /// </summary>
        public int PricePrecision { get; set; }

        /// <summary>
        /// Gets or sets the taker commission percentage.
        /// </summary>
        public decimal CommissionTakerPercent { get; set; }

        /// <summary>
        /// Gets or sets the maker commission percentage.
        /// </summary>
        public decimal CommissionMakerPercent { get; set; }
    }
}
