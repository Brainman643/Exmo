namespace Exmo.Models
{
    public class CreateOrderRequest
    {
        /// <summary>
        /// Валютная пара.
        /// </summary>
        public Pair Pair { get; set; }

        /// <summary>
        /// Количество по ордеру.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Цена по ордеру.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Тип ордера.
        /// </summary>
        public CreateOrderType Type { get; set; }
    }
}
