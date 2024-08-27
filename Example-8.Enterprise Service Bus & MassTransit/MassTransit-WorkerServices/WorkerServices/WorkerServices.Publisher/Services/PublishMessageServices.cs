using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerServices.Shared.Messages;

namespace WorkerServices.Publisher.Services
{
    public class PublishMessageServices : BackgroundService
    {
        public IPublishEndpoint _publishEndpoint;
        public PublishMessageServices(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;
            while (true)
            {
                ExampleMessage message = new ExampleMessage
                {
                    Text = $"Message {++i}"
                };
                await _publishEndpoint.Publish(message);

            }
        }
    }
}
