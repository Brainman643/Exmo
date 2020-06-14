using Newtonsoft.Json;

namespace Exmo.Models
{
    public class RequiredAmount
    {
        /// <summary>
        /// Gets or sets the quantity to buy.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets the amount to buy.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the average price.
        /// </summary>
        [JsonProperty("avg_price")]
        public decimal AveragePrice { get; set; }
    }
}
