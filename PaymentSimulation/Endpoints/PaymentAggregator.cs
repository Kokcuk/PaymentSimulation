using System.Collections.Generic;
using System.Linq;
using PaymentSimulation.Common;
using PaymentSimulation.Enums;
using PaymentSimulation.Messages;

namespace PaymentSimulation.Endpoints
{
    public class PaymentAggregator
    {
        public List<PaymentSession> PaymentSessions { get; set; }
        private readonly ILogger _logger;

        public PaymentAggregator()
        {
            _logger = Locator.Instance.GetService<ILogger>();
            PaymentSessions = new List<PaymentSession>();
        }

        public RedirectMessage Start(PaymentStartRequest request)
        {
            var paymentSession = new PaymentSession
            {
                Amount = request.Amount,
                Id = request.SessionId,
                SessionState = PaymentSessionState.Pending
            };
            PaymentSessions.Add(paymentSession);

            _logger.Log($"Payment aggregator: new session added id: {paymentSession.Id} amount: ${paymentSession.Amount}, waiting for payment");
            return new RedirectMessage {Url = paymentSession.Id};
        }

        public RedirectMessage Pay(PayRequest request)
        {
            var bank = Locator.Instance.GetService<Bank>();
            var merchant = Locator.Instance.GetService<Merchant>();

            var session = PaymentSessions.FirstOrDefault(x=>x.Id == request.SessionId);
            if(session==null)
                return new RedirectMessage { OperationResult = OperationResult.Failt, Message = "Session expired or invalid"};

            var bankChargeResponse =
                bank.AuthorizeAndCharge(new AuthorizeAndChargeRequest
                {
                    Amount = session.Amount,
                    Number = request.Number,
                    SecureCode = request.SecureCode
                });

            _logger.Log($"Payment aggregator: charging form bank account {request.Number} result: {bankChargeResponse.OperationResult} {bankChargeResponse.Message}");
            merchant.Callback(new CallbackMessage
            {
                SessionId = session.Id,
                SessionState = bankChargeResponse.OperationResult == OperationResult.Success ? PaymentSessionState.Paid : PaymentSessionState.Pending
            });

            return new RedirectMessage {Message = session.Id };
        }
    }
}
