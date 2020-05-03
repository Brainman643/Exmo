using System;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class UserOrder
    {
        /// <summary>
        /// Идентификатор ордера.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public long? OrderId { get; set; }

        /// <summary>
        /// Идентификатор стоп-ордера.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public long? ParentOrderId { get; set; }

        /// <summary>
        /// Дата и время создания ордера.
        /// </summary>
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
        [JsonProperty(Required = Required.Default)]
        public decimal? Price { get; set; }

        /// <summary>
        /// Цена, по которой исполняется стоп-ордер.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public decimal? TriggerPrice { get; set; }

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
