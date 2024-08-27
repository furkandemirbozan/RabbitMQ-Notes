using MassTransit;
using RabbitMQ_MassTransit.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_MassTransit.Cunsomer.Consumer
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelen Mesaj : {context.Message.Text}");
            return Task.CompletedTask;
        }
    }
}
