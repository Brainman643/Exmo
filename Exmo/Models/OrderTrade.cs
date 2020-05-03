using Newtonsoft.Json;

namespace Exmo.Models
{
    public class OrderTrade : Trade
    {
        /// <summary>
        /// Идентификатор ордера.
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// Идентификатор стоп-ордера.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public long? ParentOrderId { get; set; }

        /// <summary>
        /// Валютная пара.
        /// </summary>
        public Pair Pair { get; set; }

        [JsonProperty("exec_type", Required = Required.Default)]
        public ExecutionType? ExecutionType { get; set; }

        [JsonProperty(Required = Required.Default)]
        public decimal? CommissionAmount { get; set; }

        [JsonProperty(Required = Required.Default)]
        public string CommissionCurrency { get; set; }

        [JsonProperty(Required = Required.Default)]
        public decimal? CommissionPercent { get; set; }
    }
}
