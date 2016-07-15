using PaymentSimulation.Messages;

namespace PaymentSimulation.Endpoints
{
    public class Customer
    {
        private readonly ILogger _logger;

        public Customer()
        {
            _logger = Locator.Instance.GetService<ILogger>();
        }

        public OperationResultResponse Purchase(long goodId, int quantity)
        {
            _logger.Log($"Customer: new purchase goodId: {goodId} quantity:{quantity}");

            var merchant = Locator.Instance.GetService<Merchant>();
            var bank = Locator.Instance.GetService<Bank>();

            var paymentAggregator = Locator.Instance.GetService<PaymentAggregator>();
            RedirectMessage response = merchant.Purchase(new PurchaseReqeust {GoodId = goodId, Quantity = quantity});

            _logger.Log($"Customer: merchant redirect to payment aggregator, sessionId: {response.Url} {response.OperationResult} {response.Message}");
            _logger.Log($"Customer: payment aggregator payment attempt");
            var payRedirect = paymentAggregator.Pay(new PayRequest {Number = "8888", SecureCode = "1234", SessionId = response.Url});
            _logger.Log($"Customer: payment aggregator redirect to payment aggregator, sessionId: {payRedirect.Url} {payRedirect.OperationResult} {payRedirect.Message}");

            OperationResultResponse merchantFinalResult = merchant.PurchaseCompleted(payRedirect.Url);
            _logger.Log($"Customer: merchant result {merchantFinalResult.OperationResult} {merchantFinalResult.Message}");

            var balance = bank.GetBalance("8888");
            _logger.Log($"Customer: money left: ${balance}");

            return merchantFinalResult;
        }
    }
}
