using PaymentSimulation.Enums;
using PaymentSimulation.Messages;

namespace PaymentSimulation.Common
{
    public class BankAccount
    {
        private double Balance { get; set; }

        public string Number { get; set; }
        public string SecureCode { get; set; }

        public OperationResultResponse AddBalance(double amount)
        {
            Balance += amount;
            return new OperationResultResponse {OperationResult = OperationResult.Success};
        }

        public OperationResultResponse Charge(double amount)
        {
            if(Balance < amount)
                return new OperationResultResponse { OperationResult = OperationResult.Failt, Message = "Not enough money" };

            Balance = Balance - amount;
            return new OperationResultResponse { OperationResult = OperationResult.Success };
        }

        public double GetBalance()
        {
            return Balance;
        }
    }
}