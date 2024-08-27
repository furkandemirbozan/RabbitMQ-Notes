using RabbitMQ.Client;
using System.Text;


//Bağlantı oluşturma
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://lrwxleat:Y-P_U-BqIJLnMwkmq34PvtzucOCFGLkN@toad.rmq.cloudamqp.com/lrwxleat");


//Bağlantıyı aktifleştirme ve kanal Açma

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable:true);//exclusive: false olursa birden fazla bağlantıdan erişilebilir.   durable : true olursa kuyruk kalıcı olur. Yani RabbitMQ restart atsa bile kuyruk silinmez.


//Queue'ya mesaj gönderme
//RabbitMQ mesajları byte array olarak alır.Haliyle string mesajı byte array'e çevirip göndermemiz gerekiyor.

//byte[] message=Encoding.UTF8.GetBytes("Merhaba RabbitMQ");
//channel.BasicPublish(exchange:"" , routingKey:"example-queue", basicProperties:null, body:message);//exchange: mesajın hangi exchange'e gönderileceğini belirtir. routingKey: hangi queue'ya gönderileceğini belirtir. basicProperties: mesajın özelliklerini belirtir. body: mesajın içeriğini belirtir. 

IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;//mesajın kalıcı olmasını sağlar. Yani RabbitMQ restart atsa bile mesaj silinmez.

for (int i = 0; i < 100; i++)
{
    Task.Delay(500).Wait();
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba RabbitMQ {i}");
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message,basicProperties:properties);//basicProperties parametresi properties verilerek mesajın kalıcı olmasını sağladık.
    Console.WriteLine($"Mesaj gönderildi: {i}");
}
Console.Read();

