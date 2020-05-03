namespace Exmo.Models
{
    public class CreateStopMarketOrderRequest
    {
        public Pair Pair { get; set; }

        public decimal Quantity { get; set; }

        public decimal TriggerPrice { get; set; }

        public StopMarketOrderType Type { get; set; }
    }
}
