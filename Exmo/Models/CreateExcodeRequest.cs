using Newtonsoft.Json;

namespace Exmo.Models
{
    public class CreateExcodeRequest
    {
        /// <summary>
        /// Валюта купона.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Сумма купона.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Логин пользователя, который сможет загрузить купон. Если не указан, кто угодно сможет его погасить.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Login { get; set; }
    }
}
