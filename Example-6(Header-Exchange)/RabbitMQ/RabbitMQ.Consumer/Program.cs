using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;



ConnectionFactory factory = new();
factory.Uri = new("amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();



channel.ExchangeDeclare("header-exchange-example",
    type: ExchangeType.Headers);

Console.WriteLine("Lütfen Header value sunu girinizz :");
string value = Console.ReadLine();
string queueName= channel.QueueDeclare().QueueName;

channel.QueueBind(
    queue: queueName,
    exchange: "header-exchange-example",
    routingKey: string.Empty,
    arguments: new Dictionary<string, object>
    {
        ["no"] = value
    });
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine($"Gelen Mesaj : {message}");
};


Console.Read();