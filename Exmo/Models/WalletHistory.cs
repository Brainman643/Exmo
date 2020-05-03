using System;

namespace Exmo.Models
{
    public class WalletHistory
    {
        /// <summary>
        /// Начало периода.
        /// </summary>
        public DateTimeOffset Begin { get; set; }

        /// <summary>
        /// Конец периода.
        /// </summary>
        public DateTimeOffset End { get; set; }

        /// <summary>
        /// Список операций.
        /// </summary>
        public WalletOperation[] History { get; set; }
    }
}
