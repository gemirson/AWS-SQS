using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AWS.SQS.Publish.Messages;
using AWS.SQS.Publish.Model;
using Message = Amazon.SQS.Model.Message;

namespace AWS.SQS.Publish.SQS
{
    public interface IMessageBus:IDisposable
    {
        Task<List<Message>> ReceiveMessagesAsync(CancellationToken cancellationToken = default);
        Task<bool> SendMessageAsync(Event @event, CancellationToken cancellationToken = default);
        Task<bool> DeleteMessageAsync(string message, CancellationToken cancellationToken = default);
    }
}