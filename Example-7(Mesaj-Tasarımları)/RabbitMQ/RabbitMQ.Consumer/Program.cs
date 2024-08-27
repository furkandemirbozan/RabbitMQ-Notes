

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp");


using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


#region P2P (Point to Point) Tasarımı

string queueName = "example-p2p-queue";

channel.QueueDeclare(queue: queueName,
    durable: false,//durable false olursa mesajlar kalıcı olmaz. Yani RabbitMQ restart edildiğinde mesajlar silinir.
    exclusive: false,// exclusive false olursa mesajlar başka bir consumer tarafından da okunabilir.
    autoDelete: false);// autoDelete false olursa mesajlar silinmez.

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: queueName,
    autoAck: true,//autoAck true olursa mesajlar okunduktan sonra silinir.
    consumer: consumer);// consumer nesnesi oluşturulur.

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};
#endregion

#region Publish/Subscribe (Pub/Sub) Tasarımı
//string exchangeName = "example-pub-sub-exchange";
//channel.ExchangeDeclare(exchange: exchangeName,
//    type: ExchangeType.Fanout);


//string queueName = channel.QueueDeclare().QueueName;
//channel.QueueBind(
//    queue: queueName,
//    exchange: exchangeName,
//    routingKey: string.Empty);

//EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
//channel.BasicConsume(
//    queue: queueName,
//    autoAck: false,
//    consumer: consumer);

//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};

#endregion


#region Work Queue(İş Kuyruğu) Tasarımı​
//string queueName = "example-work-queue";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(
//    queue: queueName,
//    autoAck: true,
//    consumer: consumer);

//channel.BasicQos(
//    prefetchCount: 1,
//    prefetchSize: 0,
//    global: false);

//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};
#endregion

#region Request/Response Tasarımı​

//string requestQueueName = "example-request-response-queue";
//channel.QueueDeclare(
//    queue: requestQueueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(
//    queue: requestQueueName,
//    autoAck: true,
//    consumer: consumer);

//consumer.Received += (sender, e) =>
//{
//    string message = Encoding.UTF8.GetString(e.Body.Span);
//    Console.WriteLine(message);
//    //.....
//    byte[] responseMessage = Encoding.UTF8.GetBytes($"İşlem tamamlandı. : {message}");
//    IBasicProperties properties = channel.CreateBasicProperties();
//    properties.CorrelationId = e.BasicProperties.CorrelationId;
//    channel.BasicPublish(
//        exchange: string.Empty,
//        routingKey: e.BasicProperties.ReplyTo,
//        basicProperties: properties,
//        body: responseMessage);
//};

#endregion

Console.Read();