using System.Runtime.Serialization;

namespace Exmo.Models
{
    public enum Resolution
    {
        [EnumMember(Value = "1")]
        Minute,

        [EnumMember(Value = "5")]
        Minute5,

        [EnumMember(Value = "15")]
        Minute15,

        [EnumMember(Value = "30")]
        Minute30,

        [EnumMember(Value = "45")]
        Minute45,

        [EnumMember(Value = "60")]
        Hour,

        [EnumMember(Value = "120")]
        Hour2,

        [EnumMember(Value = "180")]
        Hour3,

        [EnumMember(Value = "240")]
        Hour4,

        [EnumMember(Value = "D")]
        Day,

        [EnumMember(Value = "W")]
        Week,

        [EnumMember(Value = "M")]
        Month
    }
}
