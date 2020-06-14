using Newtonsoft.Json;

namespace Exmo.Models
{
    public class OrderTrade : Trade
    {
        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// Gets or sets the stop order identifier.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public long? ParentOrderId { get; set; }

        /// <summary>
        /// Gets or sets the currency pair.
        /// </summary>
        public CurrencyPair Pair { get; set; }

        /// <summary>
        /// Gets or sets the execution type.
        /// </summary>
        [JsonProperty("exec_type", Required = Required.Default)]
        public ExecutionType? ExecutionType { get; set; }

        /// <summary>
        /// Gets or sets the commission amount.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public decimal? CommissionAmount { get; set; }

        /// <summary>
        /// Gets or sets the commission currency.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public string CommissionCurrency { get; set; }

        /// <summary>
        /// Gets or sets the commission percentage.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public decimal? CommissionPercent { get; set; }
    }
}
