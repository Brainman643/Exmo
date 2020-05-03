namespace Exmo.Models
{
    public class OrderBook
    {
        /// <summary>
        /// Объем всех ордеров на продажу.
        /// </summary>
        public decimal AskQuantity { get; set; }

        /// <summary>
        /// Сумма всех ордеров на продажу.
        /// </summary>
        public decimal AskAmount { get; set; }

        /// <summary>
        /// Минимальная цена продажи.
        /// </summary>
        public decimal AskTop { get; set; }

        /// <summary>
        /// Объем всех ордеров на покупку.
        /// </summary>
        public decimal BidQuantity { get; set; }

        /// <summary>
        /// Сумма всех ордеров на покупку.
        /// </summary>
        public decimal BidAmount { get; set; }

        /// <summary>
        /// Максимальная цена покупки.
        /// </summary>
        public decimal BidTop { get; set; }

        /// <summary>
        /// Список ордеров на покупку.
        /// </summary>
        public Order[] Bid { get; set; }

        /// <summary>
        /// Список ордеров на продажу.
        /// </summary>
        public Order[] Ask { get; set; }
    }
}
