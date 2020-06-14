using System;

namespace Exmo.Models
{
    public class Trade
    {
        /// <summary>
        /// Gets or sets the trade identifier.
        /// </summary>
        public long TradeId { get; set; }

        /// <summary>
        /// Gets or sets the trade type.
        /// </summary>
        public TradeType Type { get; set; }

        /// <summary>
        /// Gets or sets the price of the deal.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the deal.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets the amount of the deal.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date of the deal.
        /// </summary>
        public DateTimeOffset Date { get; set; }
    }
}
