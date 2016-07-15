using System.Collections.Generic;
using System.Linq;
using PaymentSimulation.Common;
using PaymentSimulation.Enums;
using PaymentSimulation.Messages;

namespace PaymentSimulation.Endpoints
{
    public class Bank
    {
        private readonly List<BankAccount> _bankAccounts;
        private readonly ILogger _logger;

        public Bank()
        {
            _logger = Locator.Instance.GetService<ILogger>();
            _bankAccounts = new List<BankAccount>();
        }

        public void AddBankAccount(BankAccount bankAccount)
        {
            _bankAccounts.Add(bankAccount);
            _logger.Log($"Bank: New bank account added {bankAccount.Number}");
        }

        public double GetBalance(string bankAccountNumber)
        {
            var bankAccount = _bankAccounts.FirstOrDefault(x => x.Number == bankAccountNumber);
            if (bankAccount == null)
                return 0;

            return bankAccount.GetBalance();
        }

        public OperationResultResponse AddBalance(double amount, string bankAccountNumber)
        {
            var bankAccount = _bankAccounts.FirstOrDefault(x => x.Number == bankAccountNumber);
            if(bankAccount==null)
                return new OperationResultResponse {OperationResult = OperationResult.Failt, Message = "Invalid bank account number"};

            var result = bankAccount.AddBalance(amount);

            _logger.Log($"Bank: ${amount} added to {bankAccount.Number}");
            return result;
        }

        public OperationResultResponse AuthorizeAndCharge(AuthorizeAndChargeRequest request)
        {
            var bankAccount =
                _bankAccounts.FirstOrDefault(x => x.Number == request.Number && x.SecureCode == request.SecureCode);

            if(bankAccount==null)
                return new OperationResultResponse { OperationResult = OperationResult.Failt, Message = "Not authorized"};

            var chargeResult = bankAccount.Charge(request.Amount);
            _logger.Log($"Bank: Charged ${request.Amount} from account {bankAccount.Number}");
            return chargeResult;
        }
    }
}
