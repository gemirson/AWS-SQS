using Flunt.Notifications;
using System;
using Flunt.Validations;

namespace AWS.SQS.Publish.Model
{
    public class OrderRequest:Notifiable
    {
        public string Name { get;  set; }
        public string Total { get;set; }
       
       
    }
}