

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri=new("amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp");


using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


#region P2P (Point to Point) Tasarımı

string queuename = "example-p2p-queue";

channel.QueueDeclare(queue: queuename,
    durable: false,
    exclusive: false,
    autoDelete: false);

byte[] message = Encoding.UTF8.GetBytes("hello, rabbitmq!");
channel.BasicPublish(exchange: string.Empty,
    routingKey: queuename,
    body: message);


#endregion

#region Publish/Subscribe (Pub/Sub) Tasarımı

//string exchangeName = "example-pub-sub-exchange";
//channel.ExchangeDeclare(exchange: exchangeName,
//    type: ExchangeType.Fanout);

//for (int i = 0; i < 100; i++)
//{
//await Task.Delay(500);
//    byte[] message = Encoding.UTF8.GetBytes("Hello, RabbitMQ!"+i);

//    channel.BasicPublish(
//        exchange: exchangeName,
//        routingKey: string.Empty,
//        body: message);
//}
#endregion

#region Work Queue(İş Kuyruğu) Tasarımı​
//string queueName = "example-work-queue";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(200);

//    byte[] message = Encoding.UTF8.GetBytes("merhaba" + i);

//    channel.BasicPublish(
//        exchange: string.Empty,
//        routingKey: queueName,
//        body: message);
//}

#endregion

#region Request/Response Tasarımı​
//string requestQueueName = "example-request-response-queue";
//channel.QueueDeclare(
//    queue: requestQueueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//string replyQueueName = channel.QueueDeclare().QueueName;

//string correlationId = Guid.NewGuid().ToString();

//#region Request Mesajını Oluşturma ve Gönderme
//IBasicProperties properties = channel.CreateBasicProperties();
//properties.CorrelationId = correlationId;
//properties.ReplyTo = replyQueueName;

//for (int i = 0; i < 10; i++)
//{
//    byte[] message = Encoding.UTF8.GetBytes("merhaba" + i);
//    channel.BasicPublish(
//        exchange: string.Empty,
//        routingKey: requestQueueName,
//        body: message,
//        basicProperties: properties);
//}
//#endregion
//#region Response Kuyruğu Dinleme
//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(
//    queue: replyQueueName,
//    autoAck: true,
//    consumer: consumer);

//consumer.Received += (sender, e) =>
//{
//    if (e.BasicProperties.CorrelationId == correlationId)
//    {
//        //....
//        Console.WriteLine($"Response : {Encoding.UTF8.GetString(e.Body.Span)}");
//    }
//};
//#endregion

#endregion


Console.Read();