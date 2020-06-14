using Newtonsoft.Json;

namespace Exmo.Models
{
    public class WithdrawTransaction
    {
        public bool Status { get; set; }

        [JsonProperty("txid")]
        public string TransactionId { get; set; }
    }
}
