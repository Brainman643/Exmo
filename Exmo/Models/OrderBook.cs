namespace Exmo.Models
{
    public class OrderBook
    {
        /// <summary>
        /// Gets or sets the volume of all sell orders.
        /// </summary>
        public decimal AskQuantity { get; set; }

        /// <summary>
        /// Gets or sets the amount of all sell orders.
        /// </summary>
        public decimal AskAmount { get; set; }

        /// <summary>
        /// Gets or sets the minimum selling price.
        /// </summary>
        public decimal AskTop { get; set; }

        /// <summary>
        /// Gets or sets the volume of all buy orders.
        /// </summary>
        public decimal BidQuantity { get; set; }

        /// <summary>
        /// Gets or sets the amount of all buy orders.
        /// </summary>
        public decimal BidAmount { get; set; }

        /// <summary>
        /// Gets or sets the maximum buying price.
        /// </summary>
        public decimal BidTop { get; set; }

        /// <summary>
        /// Gets or sets the list of the buy orders.
        /// </summary>
        public Order[] Bid { get; set; }

        /// <summary>
        /// Gets or sets the list of the sell orders.
        /// </summary>
        public Order[] Ask { get; set; }
    }
}
