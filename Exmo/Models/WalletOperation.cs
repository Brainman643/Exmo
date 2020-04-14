using System;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class WalletOperation
    {
        /// <summary>
        /// Дата операции.
        /// </summary>
        [JsonProperty("dt")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Тип.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Валюта.
        /// </summary>
        [JsonProperty("curr")]
        public string Currency { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Провайдер.
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// Сумма.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Счет.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Идентификатор транзакции, по которому ее можно найти в blockchain
        /// </summary>
        [JsonProperty("txid")]
        public string TransactionId { get; set; }
    }
}
