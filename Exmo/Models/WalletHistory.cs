using System;

namespace Exmo.Models
{
    public class WalletHistory
    {
        /// <summary>
        /// Gets or sets the beginning of the period.
        /// </summary>
        public DateTimeOffset Begin { get; set; }

        /// <summary>
        /// Gets or sets the end of the period.
        /// </summary>
        public DateTimeOffset End { get; set; }

        /// <summary>
        /// Gets or sets the operations history.
        /// </summary>
        public WalletOperation[] History { get; set; }
    }
}
