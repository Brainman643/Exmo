using Newtonsoft.Json;

namespace Exmo.Models
{
    public class LoadExcodeResult
    {
        /// <summary>
        /// Идентификатор купона.
        /// </summary>
        [JsonProperty("task_id")]
        public long TaskId { get; set; }

        /// <summary>
        /// Валюта купона.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Сумма купона.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// True, если EX-CODE загружен, но находится на проверке
        /// </summary>
        public bool Reviewing { get; set; }
    }
}
