using System.Runtime.Serialization;

namespace Exmo.Models
{
    public enum TradeType
    {
        [EnumMember(Value = "buy")]
        Buy,

        [EnumMember(Value = "sell")]
        Sell
    }
}
