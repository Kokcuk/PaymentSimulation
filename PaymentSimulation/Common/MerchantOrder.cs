using PaymentSimulation.Enums;

namespace PaymentSimulation.Common
{
    public class MerchantOrder
    {
        public long GoodId { get; set; }
        public int Quantity { get; set; }
        public string SessionId { get; set; }
        public PaymentSessionState State { get; set; }
    }
}