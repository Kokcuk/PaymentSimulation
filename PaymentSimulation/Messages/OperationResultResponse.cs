using PaymentSimulation.Enums;

namespace PaymentSimulation.Messages
{
    public class OperationResultResponse
    {
        public OperationResult OperationResult { get; set; }
        public string Message { get; set; }
    }
}