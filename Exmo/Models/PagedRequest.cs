namespace Exmo.Models
{
    public class PagedRequest
    {
        /// <summary>
        /// Get or sets the first position to return from the results of the request.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the count of the displayed positions.
        /// </summary>
        public int Limit { get; set; } = 100;
    }
}
