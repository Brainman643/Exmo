using System.Runtime.Serialization;

namespace Exmo.Models
{
    public enum ExecutionType
    {
        [EnumMember(Value = "taker")]
        Taker,

        [EnumMember(Value = "maker")]
        Maker
    }
}
