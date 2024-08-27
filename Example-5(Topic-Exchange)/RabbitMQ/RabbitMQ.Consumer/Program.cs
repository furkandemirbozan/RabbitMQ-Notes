using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;



ConnectionFactory factory = new();
factory.Uri = new("amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare(exchange: "topic-exchange-example"
    ,type: ExchangeType.Topic);


Console.WriteLine("Dinlenecek topic formatını belirtir misin : ");

string topic = Console.ReadLine();
string queueName= channel.QueueDeclare().QueueName;

channel.QueueBind(queue: queueName,
    exchange: "topic-exchange-example",
    routingKey: topic);


EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
   string message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine(message);
};

Console.Read();