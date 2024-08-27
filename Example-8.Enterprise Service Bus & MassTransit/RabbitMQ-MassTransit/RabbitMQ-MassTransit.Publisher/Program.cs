using MassTransit;
using RabbitMQ_MassTransit.Shared.Messages;

string rabbitMQUri = "amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp";

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});


ISendEndpoint sendEndpoint=await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

Console.WriteLine("Gönderilecek Mesaj: ");

string message = Console.ReadLine();
await sendEndpoint.Send<IMessage>(new ExampleMessage()
{
    Text=message
});

Console.Read();
