using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSimulation.Messages
{
    public class PaymentStartRequest
    {
        public string SessionId { get; set; }
        public double Amount { get; set; }
    }
}
