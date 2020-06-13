namespace Exmo.Models
{
    public class OrderTrades
    {
        /// <summary>
        /// Gets or sets the trade type.
        /// </summary>
        public TradeType Type { get; set; }

        /// <summary>
        /// Gets or sets the incoming currency.
        /// </summary>
        public string InCurrency { get; set; }

        /// <summary>
        /// Gets or sets the amount of the incoming currency.
        /// </summary>
        public decimal InAmount { get; set; }

        /// <summary>
        /// Gets or sets the outcoming currency.
        /// </summary>
        public string OutCurrency { get; set; }

        /// <summary>
        /// Gets or sets the amount of the outcoming currency.
        /// </summary>
        public decimal OutAmount { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="OrderTrade"/>.
        /// </summary>
        public OrderTrade[] Trades { get; set; }
    }
}
