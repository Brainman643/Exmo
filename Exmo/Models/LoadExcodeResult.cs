namespace Exmo.Models
{
    public class LoadExcodeResult
    {
        /// <summary>
        /// Gets or sets the task identifier.
        /// </summary>
        public long TaskId { get; set; }

        /// <summary>
        /// Gets or sets the coupon currency.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the amount in the coupon.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the indicator which shows whether the EXCODE coupon is uploaded but under review or reviewing is not needed and the payment was successful.
        /// </summary>
        /// <remarks>
        /// <c>true</c> – if EX-CODE is uploaded but is currently under review.
        /// <c>false</c> – if the reviewing is not needed and the payment was successful.
        /// </remarks>
        public bool Reviewing { get; set; }
    }
}
