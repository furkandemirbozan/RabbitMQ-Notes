using RabbitMQ.Client;
using System.Text;



ConnectionFactory factory = new();
factory.Uri = new("amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare("header-exchange-example",
    type: ExchangeType.Headers);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Message {i}");
    Console.WriteLine("Lütfen Header Value'sunu gir : ");
    string value = Console.ReadLine();

    IBasicProperties basicProperties = channel.CreateBasicProperties();
    basicProperties.Headers = new Dictionary<string, object>
    {
         ["no"]= value 
    };

    channel.BasicPublish(exchange:"header-exchange-example",
        routingKey: string.Empty,
        body: message,
        basicProperties: basicProperties
        );
}


Console.Read();