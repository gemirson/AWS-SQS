namespace AWS.SQS.Publish.Config
{
    public class ConfigSQS
    {
        public string QueueUrl { get; set; }
        public string DlQueueUrl { get; set; }
        public string FifoQueueUrl { get; set; }
        public string Region { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
    
}