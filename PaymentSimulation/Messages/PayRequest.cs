namespace PaymentSimulation.Messages
{
    public class PayRequest
    {
        public string SessionId { get; set; }
        public string Number { get; set; }
        public string SecureCode { get; set; }
    }
}