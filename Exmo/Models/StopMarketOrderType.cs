using System.Runtime.Serialization;

namespace Exmo.Models
{
    public enum StopMarketOrderType
    {
        [EnumMember(Value = "buy")]
        Buy,

        [EnumMember(Value = "sell")]
        Sell
    }
}
