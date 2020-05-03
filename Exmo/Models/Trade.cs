using System;

namespace Exmo.Models
{
    public class Trade
    {
        /// <summary>
        /// Идентификатор сделки.
        /// </summary>
        public long TradeId { get; set; }

        /// <summary>
        /// Тип сделки.
        /// </summary>
        public TradeType Type { get; set; }

        /// <summary>
        /// Цена сделки.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Количество по сделке.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Сумма сделки.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Дата и время сделки.
        /// </summary>
        public DateTimeOffset Date { get; set; }
    }
}
