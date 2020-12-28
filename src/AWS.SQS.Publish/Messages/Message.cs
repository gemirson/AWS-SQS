using System;

namespace AWS.SQS.Publish.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public Guid MessageId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
            MessageId = Guid.NewGuid();
        }
    }
}