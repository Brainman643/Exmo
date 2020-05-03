namespace Exmo.Models
{
    public class PairSettings
    {
        /// <summary>
        /// Минимальное кол-во по ордеру.
        /// </summary>
        public decimal MinQuantity { get; set; }

        /// <summary>
        /// Максимальное кол-во по ордеру.
        /// </summary>
        public decimal MaxQuantity { get; set; }

        /// <summary>
        /// Минимальная цена по ордеру.
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// Максимальная цена по ордеру.
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// Минимальная сумма по ордеру.
        /// </summary>
        public decimal MinAmount { get; set; }

        /// <summary>
        /// Максимальная сумма по ордеру.
        /// </summary>
        public decimal MaxAmount { get; set; }

        /// <summary>
        /// Displays how many decimal places can be used in the price when placing an order.
        /// </summary>
        public int PricePrecision { get; set; }

        /// <summary>
        /// Taker commission in percent.
        /// </summary>
        public decimal CommissionTakerPercent { get; set; }

        /// <summary>
        /// Maker commission in percent.
        /// </summary>
        public decimal CommissionMakerPercent { get; set; }
    }
}
