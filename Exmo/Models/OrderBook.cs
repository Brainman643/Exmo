using Newtonsoft.Json;

namespace Exmo.Models
{
    public class OrderBook
    {
        /// <summary>
        /// Объем всех ордеров на продажу.
        /// </summary>
        [JsonProperty("ask_quantity")]
        public decimal AskQuantity { get; set; }

        /// <summary>
        /// Сумма всех ордеров на продажу.
        /// </summary>
        [JsonProperty("ask_amount")]
        public decimal AskAmount { get; set; }

        /// <summary>
        /// Минимальная цена продажи.
        /// </summary>
        [JsonProperty("ask_top")]
        public decimal AskTop { get; set; }

        /// <summary>
        /// Объем всех ордеров на покупку.
        /// </summary>
        [JsonProperty("bid_quantity")]
        public decimal BidQuantity { get; set; }

        /// <summary>
        /// Сумма всех ордеров на покупку.
        /// </summary>
        [JsonProperty("bid_amount")]
        public decimal BidAmount { get; set; }

        /// <summary>
        /// Максимальная цена покупки.
        /// </summary>
        [JsonProperty("bid_top")]
        public decimal BidTop { get; set; }

        /// <summary>
        /// Список ордеров на покупку.
        /// </summary>
        [JsonProperty("bid")]
        public Order[] Bid { get; set; }

        /// <summary>
        /// Список ордеров на продажу.
        /// </summary>
        [JsonProperty("ask")]
        public Order[] Ask { get; set; }
    }
}
