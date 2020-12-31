using System.Threading;
using System.Threading.Tasks;
using AWS.SQS.Publish.SQS;
using MediatR;

namespace AWS.SQS.Publish.Events.Order
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatOrderEventHandler : INotificationHandler<CreatedOrderEvent>
    {
        private readonly IMessageBus _messageBus;

        public CreatOrderEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(CreatedOrderEvent notification, CancellationToken cancellationToken)
        {
           await _messageBus.SendMessageAsync(notification,cancellationToken);
        }
    }
}