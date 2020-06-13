using Newtonsoft.Json;

namespace Exmo.Models
{
    public class OrderBookRequest
    {
        /// <summary>
        /// Gets or sets the currency pairs.
        /// </summary>
        [JsonProperty("pair")]
        public CurrencyPairCollection Pairs { get; set; }

        /// <summary>
        /// Gets or sets the count of the displayed positions.
        /// </summary>
        public int Limit { get; set; } = 100;
    }
}
