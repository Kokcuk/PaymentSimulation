using System;
using PaymentSimulation.Common;
using PaymentSimulation.Endpoints;

namespace PaymentSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new ConsoleLogger();
            Locator.Instance.Register(typeof(ILogger), logger);

            var merchant = new Merchant();
            var bank = new Bank();
            var paymentAggregator = new PaymentAggregator();
            var customer = new Customer();

            Locator.Instance.Register(merchant);
            Locator.Instance.Register(bank);
            Locator.Instance.Register(paymentAggregator);
            Locator.Instance.Register(customer);

            var good = new Good
            {
                GoodId = 12,
                Price = 39.99,
                Name = "Carrot"
            };

            var bankAccount = new BankAccount
            {
                Number = "8888",
                SecureCode = "1234"
            };
            bank.AddBankAccount(bankAccount);
            bank.AddBalance(500, "8888");
            merchant.AddGood(good);

            var purchaseResult = customer.Purchase(12, 3);

            Console.ReadLine();
        }
    }
}
