using System;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class Ticker
    {
        /// <summary>
        /// Gets or sets the current maximum buying price.
        /// </summary>
        public decimal BuyPrice { get; set; }

        /// <summary>
        /// Gets or sets the current minimum selling price.
        /// </summary>
        public decimal SellPrice { get; set; }

        /// <summary>
        /// Gets or sets the last trade price.
        /// </summary>
        public decimal LastTrade { get; set; }

        /// <summary>
        /// Gets or sets the maximum trade price in 24 hours.
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// Gets or sets the minimum trade price in 24 hours.
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// Gets or sets the average trade price in 24 hours.
        /// </summary>
        public decimal Avg { get; set; }

        /// <summary>
        /// Gets or sets the volume of all trades in a base currency in 24 hours.
        /// </summary>
        [JsonProperty("vol")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets the volume of all trades in a quote currency in 24 hours.
        /// </summary>
        [JsonProperty("vol_curr")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date and time of the data update.
        /// </summary>
        public DateTimeOffset Updated { get; set; }
    }
}
