using System;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class Ticker
    {
        /// <summary>
        /// Текущая максимальная цена покупки.
        /// </summary>
        [JsonProperty("buy_price")]
        public decimal BuyPrice { get; set; }

        /// <summary>
        /// Текущая минимальная цена продажи.
        /// </summary>
        [JsonProperty("sell_price")]
        public decimal SellPrice { get; set; }

        /// <summary>
        /// Цена последней сделки.
        /// </summary>
        [JsonProperty("last_trade")]
        public decimal LastTrade { get; set; }

        /// <summary>
        /// Максимальная цена сделки за 24 часа.
        /// </summary>
        [JsonProperty("high")]
        public decimal High { get; set; }

        /// <summary>
        /// Минимальная цена сделки за 24 часа.
        /// </summary>
        [JsonProperty("low")]
        public decimal Low { get; set; }

        /// <summary>
        /// Средняя цена сделки за 24 часа.
        /// </summary>
        [JsonProperty("avg")]
        public decimal Avg { get; set; }

        /// <summary>
        /// Объем всех сделок за 24 часа.
        /// </summary>
        [JsonProperty("vol")]
        public decimal Volume { get; set; }

        /// <summary>
        /// Сумма всех сделок за 24 часа.
        /// </summary>
        [JsonProperty("vol_curr")]
        public decimal VolumeCurrent { get; set; }

        /// <summary>
        /// Дата и время обновления данных.
        /// </summary>
        [JsonProperty("updated")]
        public DateTime Updated { get; set; }
    }
}
