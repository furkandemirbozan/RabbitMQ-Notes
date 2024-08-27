using RabbitMQ.Client;
using System.Text;







ConnectionFactory factory = new();
factory.Uri = new("amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


// Exchange oluşturuyoruz.
channel.ExchangeDeclare(exchange: "fanout-exchange-examlpe", type: ExchangeType.Fanout);


for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Message {i}");
    channel.BasicPublish(
        exchange: "fanout-exchange-examlpe",
        routingKey: string.Empty,
        body: message);
}



Console.Read();