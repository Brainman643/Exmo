using System.Runtime.Serialization;

namespace Exmo.Models
{
    public enum OrderType
    {
        [EnumMember(Value = "buy")]
        Buy,

        [EnumMember(Value = "sell")]
        Sell,

        [EnumMember(Value = "market_buy")]
        MarketBuy,

        [EnumMember(Value = "market_sell")]
        MarketSell,

        [EnumMember(Value = "market_buy_total")]
        MarketBuyTotal,

        [EnumMember(Value = "market_sell_total")]
        MarketSellTotal
    }
}
