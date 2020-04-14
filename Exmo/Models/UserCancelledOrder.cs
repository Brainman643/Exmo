using System;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class UserCancelledOrder
    {
        /// <summary>
        /// Дата и время отмены ордера.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Идентификатор ордера.
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }

        /// <summary>
        /// Тип ордера.
        /// </summary>
        [JsonProperty("order_type")]
        public OrderType OrderType { get; set; }

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
        /// Сумма по ордеру.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
