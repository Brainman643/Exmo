namespace Exmo.Models
{
    public class PagedRequest
    {
        /// <summary>
        /// Смещение от последней сделки.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Количество возвращаемых сделок.
        /// </summary>
        public int Limit { get; set; } = 100;
    }
}
