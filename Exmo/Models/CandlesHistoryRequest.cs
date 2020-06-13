using System;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class CandlesHistoryRequest
    {
        /// <summary>
        /// Gets or sets the currency pair.
        /// </summary>
        [JsonProperty("symbol")]
        public CurrencyPair Pair { get; set; }

        /// <summary>
        /// Gets or sets the discreteness of candles.
        /// </summary>
        public Resolution Resolution { get; set; }

        /// <summary>
        /// Gets or sets the beginning of the period.
        /// </summary>
        public DateTimeOffset From { get; set; }

        /// <summary>
        /// Gets or sets the end of the period.
        /// </summary>
        public DateTimeOffset To { get; set; }
    }
}
