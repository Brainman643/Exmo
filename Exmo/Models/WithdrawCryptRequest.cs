using Newtonsoft.Json;

namespace Exmo.Models
{
    public class WithdrawCryptRequest
    {
        /// <summary>
        /// Количество выводимой валюты.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Выводимая валюта.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Адрес вывода.
        /// </summary>
        public string Address { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Invoice { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Transport { get; set; }
    }
}
