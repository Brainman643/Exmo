using System;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class Trade
    {
        /// <summary>
        /// Идентификатор сделки.
        /// </summary>
        [JsonProperty("trade_id")]
        public long TradeId { get; set; }

        /// <summary>
        /// Тип сделки.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Цена сделки.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Кол-во по сделке.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Сумма сделки.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Дата и время сделки.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
