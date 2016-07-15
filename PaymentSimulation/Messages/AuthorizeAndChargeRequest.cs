namespace PaymentSimulation.Messages
{
    public class AuthorizeAndChargeRequest
    {
        public double Amount { get; set; }
        public string Number { get; set; }
        public string SecureCode { get; set; }
    }
}