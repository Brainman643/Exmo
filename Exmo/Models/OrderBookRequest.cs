using Newtonsoft.Json;

namespace Exmo.Models
{
    public class OrderBookRequest
    {
        /// <summary>
        /// Валютные пары.
        /// </summary>
        [JsonProperty("pair")]
        public PairCollection Pairs { get; set; }

        /// <summary>
        /// Количество отображаемых позиций.
        /// </summary>
        public int Limit { get; set; } = 100;
    }
}
