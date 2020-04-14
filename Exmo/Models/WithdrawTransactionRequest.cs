using Newtonsoft.Json;

namespace Exmo.Models
{
    internal class WithdrawTransactionRequest
    {
        /// <summary>
        /// Идентификатор купона.
        /// </summary>
        [JsonProperty("task_id")]
        public long TaskId { get; set; }
    }
}
