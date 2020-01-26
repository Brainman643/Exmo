using Newtonsoft.Json;

namespace Exmo.Models
{
    public class PairSettings
    {
        /// <summary>
        /// Минимальное кол-во по ордеру.
        /// </summary>
        [JsonProperty("min_quantity")]
        public decimal MinQuantity { get; set; }

        /// <summary>
        /// Максимальное кол-во по ордеру.
        /// </summary>
        [JsonProperty("max_quantity")]
        public decimal MaxQuantity { get; set; }

        /// <summary>
        /// Минимальная цена по ордеру.
        /// </summary>
        [JsonProperty("min_price")]
        public decimal MinPrice { get; set; }

        /// <summary>
        /// Максимальная цена по ордеру.
        /// </summary>
        [JsonProperty("max_price")]
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// Минимальная сумма по ордеру.
        /// </summary>
        [JsonProperty("min_amount")]
        public decimal MinAmount { get; set; }

        /// <summary>
        /// Максимальная сумма по ордеру.
        /// </summary>
        [JsonProperty("max_amount")]
        public decimal MaxAmount { get; set; }
    }
}
