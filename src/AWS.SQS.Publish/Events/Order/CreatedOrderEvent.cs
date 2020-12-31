using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS.SQS.Publish.Messages;
using MediatR;

namespace AWS.SQS.Publish.Events.Order
{
    public class CreatedOrderEvent:Event
    {
        public Guid IdOrder { get; private set; }
        public string Total { get; private set; }
        public string Name { get; private set; } 

        public CreatedOrderEvent(string name,string total)
        {
            IdOrder = Guid.NewGuid();
            Name = name;
            Total = total;
        }

    }
}
