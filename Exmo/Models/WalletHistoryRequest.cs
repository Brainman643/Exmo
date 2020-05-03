using System;

namespace Exmo.Models
{
    public class WalletHistoryRequest
    {
        /// <summary>
        /// Дата, за которую нужно получить историю.
        /// </summary>
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
    }
}
