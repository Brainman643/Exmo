using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Exmo.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TradeType
    {
        [EnumMember(Value = "buy")]
        Buy,

        [EnumMember(Value = "sell")]
        Sell
    }
}
