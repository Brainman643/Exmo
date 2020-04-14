using Newtonsoft.Json;

namespace Exmo.Models
{
    public class OrderTrades
    {
        /// <summary>
        /// Тип ордера.
        /// </summary>
        public OrderType Type { get; set; }

        /// <summary>
        /// Входящая валюта.
        /// </summary>
        [JsonProperty("in_currency")]
        public string InCurrency { get; set; }

        /// <summary>
        /// Количество входящей валюты.
        /// </summary>
        [JsonProperty("in_amount")]
        public decimal InAmount { get; set; }

        /// <summary>
        /// Исходящая валюта.
        /// </summary>
        [JsonProperty("out_currency")]
        public string OutCurrency { get; set; }

        /// <summary>
        /// Количество исходящей валюты.
        /// </summary>
        [JsonProperty("out_amount")]
        public decimal OutAmount { get; set; }

        /// <summary>
        /// Список сделок.
        /// </summary>
        public OrderTrade[] Trades { get; set; }
    }
}
