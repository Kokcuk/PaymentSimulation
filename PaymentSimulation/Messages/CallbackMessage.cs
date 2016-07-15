using PaymentSimulation.Enums;

namespace PaymentSimulation.Messages
{
    public class CallbackMessage
    {
        public string SessionId { get; set; }
        public PaymentSessionState SessionState { get; set; }
    }
}