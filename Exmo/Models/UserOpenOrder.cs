using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Exmo.Models
{
    public class UserOpenOrder
    {
        /// <summary>
        /// Идентификатор ордера.
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// Дата и время создания ордера.
        /// </summary>
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        /// <summary>
        /// Тип ордера.
        /// </summary>
        public OrderType Type { get; set; }

        /// <summary>
        /// Валютная пара.
        /// </summary>
        public Pair Pair { get; set; }

        /// <summary>
        /// Цена по ордеру.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Количество по ордеру.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Количество по ордеру.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
