using System;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class CandlesHistoryRequest
    {
        /// <summary>
        /// Currency pair.
        /// </summary>
        [JsonProperty("symbol")]
        public Pair Pair { get; set; }

        /// <summary>
        /// Discreteness of candles.
        /// </summary>
        public Resolution Resolution { get; set; }

        /// <summary>
        /// Beginning of period.
        /// </summary>
        public DateTimeOffset From { get; set; }

        /// <summary>
        /// End of period.
        /// </summary>
        public DateTimeOffset To { get; set; }
    }
}
