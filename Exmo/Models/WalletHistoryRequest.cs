using System;

namespace Exmo.Models
{
    public class WalletHistoryRequest
    {
        /// <summary>
        /// Gets or sets the date to get the history.
        /// </summary>
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
    }
}
