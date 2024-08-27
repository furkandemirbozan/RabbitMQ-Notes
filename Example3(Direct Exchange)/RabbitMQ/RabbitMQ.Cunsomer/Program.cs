using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;






ConnectionFactory factory = new();
factory.Uri = new("amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp");
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


//1. Adım 
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

//2. Adım
string queueName = channel.QueueDeclare().QueueName;



channel.QueueBind(
    queue: queueName,
    exchange: "direct-exchange-example",
    routingKey: "direct-queue-example");




EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message= Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine($"Message: {message}");
};

Console.Read();

//1. Adım : Publisher'da ki exchange ile birebir aynı isim ve type'a sahip bir exchange tanımlanmalıdır!

//2. Adım : Publisher tarafından routing key'de bulunan değerdeki kuyruğa gönderilen mesajları, kendi oluşturduğumuz kuyruğa yönlendirerek tüketmemiz gerekmektedir. Bunun için öncelikle bir kuyruk oluşturulmalıdır!


