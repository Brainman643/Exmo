using System;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class WalletOperation
    {
        /// <summary>
        /// Gets or sets date and time of the operation.
        /// </summary>
        [JsonProperty("dt")]
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Gets or sets the operation type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the operation currency.
        /// </summary>
        [JsonProperty("curr")]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the operation status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier by which it can be found in the blockchain.
        /// </summary>
        [JsonProperty("txid")]
        public string TransactionId { get; set; }
    }
}
