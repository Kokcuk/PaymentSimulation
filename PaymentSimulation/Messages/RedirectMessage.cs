namespace PaymentSimulation.Messages
{
    public class RedirectMessage: OperationResultResponse
    {
        public string Url { get; set; }
    }
}