using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using AWS.SQS.Publish.Config;
using AWS.SQS.Publish.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Message = Amazon.SQS.Model.Message;

namespace AWS.SQS.Publish.SQS
{
    public class SQSFifo : IMessageBus
    {
        private readonly IAmazonSQS _sqs;
        private readonly ILogger<SQSFifo> _logger;
        private readonly ConfigSQS _configSqs;

        public SQSFifo(IAmazonSQS sqsClient, ILogger<SQSFifo> logger, IOptions<ConfigSQS> options)
        {
            _sqs = sqsClient;
            _configSqs = options.Value;
            _logger = logger;
            _logger.LogDebug(default(EventId), $"NLog injected into {nameof(SQSFifo)}");
            
        }

        public async Task<bool> DeleteMessageAsync(string messageReceiptHandle,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var deleteResult = await _sqs.DeleteMessageAsync( new DeleteMessageRequest
                {
                    QueueUrl = _configSqs.FifoQueueUrl,
                    ReceiptHandle = messageReceiptHandle
                },cancellationToken:cancellationToken);
                return deleteResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), $"Found fails to {nameof(SQSFifo)} in DeleteMessageAsync{ex.Message}");
                throw;
            }
        }
        public async Task<List<Message>> ReceiveMessagesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
               var result = await _sqs.ReceiveMessageAsync(new ReceiveMessageRequest
                {
                    QueueUrl = _configSqs.QueueUrl,
                    MaxNumberOfMessages = 10,
                    WaitTimeSeconds = 5,
                },cancellationToken);

                return result.Messages.Any() ? result.Messages : new List<Message>();
            }
            catch (AmazonSQSException ex)
            {
                _logger.LogError(default(EventId), $"Found fails to ReceiveMessageAsync a queue {nameof(SQSFifo)} in ReceiveMessagesAsync:With  Exception :{ex.Message}");
                 throw ;
            }
           
        }

        public async  Task<bool> SendMessageAsync(Event @event, CancellationToken cancellationToken = default)
        {
            try
            {
                var message = JsonConvert.SerializeObject(@event);
                var sendResult = await _sqs.SendMessageAsync(new SendMessageRequest
                {
                  
                    QueueUrl = _configSqs.QueueUrl,
                    MessageBody = message
                },cancellationToken:cancellationToken);

                return sendResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), $"Found fails to SendMessageAsync {nameof(SQSFifo)} a message to queue: With Exception : {ex.Message}");
                throw ;
            }
        }

        public void Dispose()
        {
            _sqs?.Dispose();
            
        }
    }
}