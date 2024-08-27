using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerServices.Shared.Messages;

namespace WorkerServices.Consumer.Conusmer
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
