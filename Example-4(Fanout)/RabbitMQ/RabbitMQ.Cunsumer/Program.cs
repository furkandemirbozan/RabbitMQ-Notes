using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;







ConnectionFactory factory = new();
factory.Uri = new("amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


// Exchange oluşturuyoruz.
channel.ExchangeDeclare(exchange: "fanout-exchange-examlpe", type: ExchangeType.Fanout);

// Kuyruk adını girerek kuyruğu oluşturuyoruz.
Console.Write("Kuyruk adını Giriniz ");
string _queueName = Console.ReadLine();

// Kuyruğu oluşturuyoruz.
channel.QueueDeclare(queue: _queueName,
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

// Kuyruğu exchange ile bağlıyoruz.
channel.QueueBind(queue: _queueName,
    exchange: "fanout-exchange-examlpe",
    routingKey: string.Empty);


EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine($"Gelen Mesaj: {message}");
};

Console.Read();