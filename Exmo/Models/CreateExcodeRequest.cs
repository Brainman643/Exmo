using Newtonsoft.Json;

namespace Exmo.Models
{
    public class CreateExcodeRequest
    {
        /// <summary>
        /// Gets or sets the coupon currency.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the amount in the coupon.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the user's login, who can upload the EXCODE coupon (not necessary parameter, if it is present, only this user or its creator can upload the coupon).
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Login { get; set; }
    }
}
