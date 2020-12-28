using Amazon.SQS;
using AWS.SQS.Publish.Bus;
using AWS.SQS.Publish.Config;
using AWS.SQS.Publish.Events.Order;
using AWS.SQS.Publish.SQS;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AWS.SQS.Publish.Setup
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration config)
        {

            // AWS config and inject
            services.Configure<ConfigSQS>(config.GetSection("ConfigSQS"));
            services.AddAWSService<IAmazonSQS>();

            // Bus (Mediator)
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            services.AddScoped<IMessageBus, SQSFifo>();
            services.AddScoped<INotificationHandler<CreatedOrderEvent>, CreatOrderEventHandler>();

            return services;
        }
    }
}