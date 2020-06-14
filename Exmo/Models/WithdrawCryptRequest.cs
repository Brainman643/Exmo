using Newtonsoft.Json;

namespace Exmo.Models
{
    public class WithdrawCryptRequest
    {
        /// <summary>
        /// Gets or sets amount of currency to be withdrawn.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets name of the currency to be withdrawn.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets withdrawal address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets additional identifier.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Invoice { get; set; }

        /// <summary>
        /// Gets or sets the network in which the withdrawal will be made. If you do not specify, then the default network will be selected.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Transport { get; set; }
    }
}
