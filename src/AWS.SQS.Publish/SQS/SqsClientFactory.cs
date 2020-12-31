using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using AWS.SQS.Publish.Config;
using Microsoft.Extensions.Options;

namespace AWS.SQS.Publish.SQS
{
    public class SqsClientFactory
    {
        private readonly ConfigSQS _appConfig;
        
        public SqsClientFactory(IOptions<ConfigSQS> appConfig)
        {
            _appConfig = appConfig.Value;
        }

        public  AmazonSQSClient CreateClient
        {
            get
            {
                var sqsConfig = new AmazonSQSConfig
                {
                    RegionEndpoint = RegionEndpoint.GetBySystemName(_appConfig.Region),
                    ServiceURL =  _appConfig.FifoQueueUrl
                    
                };
                var awsCredentials = new AwsCredentials(_appConfig);
                return new AmazonSQSClient(awsCredentials, sqsConfig);
            }
        }
    }
}