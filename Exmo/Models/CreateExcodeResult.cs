using System.Collections.Generic;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class CreateExcodeResult
    {
        /// <summary>
        /// Идентификатор купона.
        /// </summary>
        public long TaskId { get; set; }

        /// <summary>
        /// Код купона EXCODE.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Сумма купона.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Валюта купона.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Логин пользователя, который сможет загрузить купон. Если не указан, кто угодно сможет его погасить.
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public string Login { get; set; }

        /// <summary>
        /// Комиссия за создание купона.
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// Баланс пользователя после создания купона.
        /// </summary>
        public Dictionary<string, decimal> Balances { get; set; }
    }
}
