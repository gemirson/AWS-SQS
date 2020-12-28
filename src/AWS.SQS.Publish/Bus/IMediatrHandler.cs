using System.Threading.Tasks;
using AWS.SQS.Publish.Messages;

namespace AWS.SQS.Publish.Bus
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
    }
}