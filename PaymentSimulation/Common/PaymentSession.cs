using PaymentSimulation.Enums;

namespace PaymentSimulation.Common
{
    public class PaymentSession
    {
        public string Id { get; set; }
        public double Amount { get; set; }
        public PaymentSessionState SessionState { get; set; }
    }
}