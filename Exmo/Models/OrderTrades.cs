namespace Exmo.Models
{
    public class OrderTrades
    {
        /// <summary>
        /// Trade type.
        /// </summary>
        public TradeType Type { get; set; }

        /// <summary>
        /// Входящая валюта.
        /// </summary>
        public string InCurrency { get; set; }

        /// <summary>
        /// Количество входящей валюты.
        /// </summary>
        public decimal InAmount { get; set; }

        /// <summary>
        /// Исходящая валюта.
        /// </summary>
        public string OutCurrency { get; set; }

        /// <summary>
        /// Количество исходящей валюты.
        /// </summary>
        public decimal OutAmount { get; set; }

        /// <summary>
        /// Список сделок.
        /// </summary>
        public OrderTrade[] Trades { get; set; }
    }
}
