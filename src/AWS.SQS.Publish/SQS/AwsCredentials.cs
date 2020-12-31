using Amazon.Runtime;
using AWS.SQS.Publish.Config;
using Microsoft.Extensions.Options;

namespace AWS.SQS.Publish.SQS
{
    public class AwsCredentials : AWSCredentials
    {
        private readonly ConfigSQS _appConfig;

        public AwsCredentials(ConfigSQS  appConfig)
        {
            _appConfig = appConfig;
        }

        public override ImmutableCredentials GetCredentials()
        {
            return new ImmutableCredentials(_appConfig.AccessKey, _appConfig.SecretKey, null);
        }
    }
}