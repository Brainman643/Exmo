using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Exmo.Models
{
    public class CreateOrderRequest
    {
        /// <summary>
        /// Валютная пара.
        /// </summary>
        public Pair Pair { get; set; }

        /// <summary>
        /// Кол-во по ордеру.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Цена по ордеру.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Тип ордера.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType Type { get; set; }
    }
}
