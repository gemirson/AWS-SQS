using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS.SQS.Publish.Messages;

namespace AWS.SQS.Publish.Events.Order
{
    public class CreatedOrderEvent:Event
    {
        public Guid IdOrder { get; private set; }
        public decimal Total { get; private set; }

        public CreatedOrderEvent(Guid idOrder, decimal total)
        {
            IdOrder = idOrder;
            Total = total;
        }

    }
}
