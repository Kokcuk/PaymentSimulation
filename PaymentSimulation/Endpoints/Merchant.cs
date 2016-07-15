using System;
using System.Collections.Generic;
using System.Linq;
using PaymentSimulation.Common;
using PaymentSimulation.Enums;
using PaymentSimulation.Messages;

namespace PaymentSimulation.Endpoints
{
    public class Merchant
    {
        public List<Good> Goods { get; private set; }
        public List<MerchantOrder> MerchantOrders { get; set; }
        private readonly ILogger _logger;

        public Merchant()
        {
            _logger = Locator.Instance.GetService<ILogger>();
            Goods = new List<Good>();
            MerchantOrders = new List<MerchantOrder>();
        }

        public void AddGood(Good good)
        {
            Goods.Add(good);
            _logger.Log($"Merchant: new good added '{good.Name}' price: {good.Price}");
        }

        public RedirectMessage Purchase(PurchaseReqeust reqeust)
        {
            var paymentAggregator = Locator.Instance.GetService<PaymentAggregator>();

            var sessionId = Guid.NewGuid().ToString();

            var good = Goods.FirstOrDefault(x => x.GoodId == reqeust.GoodId);
            if(good == null)
                return new RedirectMessage { OperationResult = OperationResult.Failt, Message = "Good not found" };

            var totalCharge = good.Price*reqeust.Quantity;

            MerchantOrders.Add(new MerchantOrder
            {
                GoodId = good.GoodId,
                SessionId = sessionId,
                Quantity = reqeust.Quantity,
                State = PaymentSessionState.Pending
            });
            _logger.Log($"Merchant: new order added {good.Name} #{reqeust.Quantity}");

            RedirectMessage redirectMessage =
                paymentAggregator.Start(new PaymentStartRequest {SessionId = sessionId, Amount = totalCharge});

            return redirectMessage;
        }

        public void Callback(CallbackMessage message)
        {
            var order = MerchantOrders.FirstOrDefault(x => x.SessionId == message.SessionId);
            if (order != null)
            {
                order.State = message.SessionState;
                _logger.Log($"Merchant: payment status update session: {order.SessionId} state: {order.State}");
            }
        }

        public OperationResultResponse PurchaseCompleted(string sessionId)
        {
            var order = MerchantOrders.FirstOrDefault(x => x.SessionId == sessionId);
            if(order == null)
                return new OperationResultResponse {OperationResult = OperationResult.Failt, Message = "Session expired or invalid"};

            if(order.State == PaymentSessionState.Paid)
                return new OperationResultResponse {OperationResult = OperationResult.Success, Message = "Paid successfully" };
            
            return new OperationResultResponse { OperationResult = OperationResult.Success, Message = "Paid error" };
        }
    }
}
