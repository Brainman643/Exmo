namespace Exmo.Models
{
    public class RequiredAmountRequest
    {
        /// <summary>
        /// Валютная пара.
        /// </summary>
        public Pair Pair { get; set; }

        /// <summary>
        /// Количество, которое необходимо купить.
        /// </summary>
        public decimal Quantity { get; set; }
    }
}
