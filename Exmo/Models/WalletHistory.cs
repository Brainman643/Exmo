using System;

namespace Exmo.Models
{
    public class WalletHistory
    {
        /// <summary>
        /// Начало периода.
        /// </summary>
        public DateTime Begin { get; set; }

        /// <summary>
        /// Конец периода.
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// Список операций.
        /// </summary>
        public WalletOperation[] History { get; set; }
    }
}
