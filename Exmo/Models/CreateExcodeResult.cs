using System.Collections.Generic;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class CreateExcodeResult
    {
        /// <summary>
        /// Gets or sets the task identifier.
        /// </summary>
        public long TaskId { get; set; }

        /// <summary>
        /// Gets or sets the code of the EXCODE coupon.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the amount in the coupon.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the coupon currency.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the user's login, who can upload the EXCODE coupon (not necessary parameter, if it is present, only this user or its creator can upload the coupon).
        /// </summary>
        [JsonProperty(Required = Required.Default)]
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the commission for the EXCODE coupon creation.
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// Gets or sets the user's balances after the EXCODE coupon creation.
        /// </summary>
        public Dictionary<string, decimal> Balances { get; set; }
    }
}
