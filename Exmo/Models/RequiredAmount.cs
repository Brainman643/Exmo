using Newtonsoft.Json;

namespace Exmo.Models
{
    public class RequiredAmount
    {
        /// <summary>
        /// Количество, которое вы сможете купить.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Сумма, которую вы потратите на покупку.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Средняя цена покупки.
        /// </summary>
        [JsonProperty("avg_price")]
        public decimal AveragePrice { get; set; }
    }
}
