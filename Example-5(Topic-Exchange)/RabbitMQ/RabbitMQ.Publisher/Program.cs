using RabbitMQ.Client;
using System.Text;



ConnectionFactory factory = new();
factory.Uri = new("amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();



//Topic Exchange
channel.ExchangeDeclare(exchange: "topic-exchange-example",type: ExchangeType.Topic, durable: true);




for (int i = 0; i < 100; i++)
{
    await Task.Delay(300);
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Message {i}");
    Console.WriteLine("Topic Belirtir misin  : ");
    string topic = Console.ReadLine();
    channel.BasicPublish(exchange: "topic-exchange-example",
        routingKey: topic,
        body: messageBodyBytes);
}



Console.Read();