using Newtonsoft.Json;

namespace Exmo.Models
{

    internal class WithdrawCryptResult : Result
    {
        /// <summary>
        /// Идентификатор задачи на вывод.
        /// </summary>
        [JsonProperty("task_id")]
        public long TaskId { get; set; }
    }
}
