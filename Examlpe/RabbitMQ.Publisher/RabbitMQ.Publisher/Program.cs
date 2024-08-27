using RabbitMQ.Client;
using System.Text;


//Bağlantı oluşturma
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://dozwyqqg:dmvpzcZvndpTgQ-jTzp3AuaN1321JCu_@toad.rmq.cloudamqp.com/dozwyqqg");


//Bağlantıyı aktifleştirme ve kanal Açma

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);//exclusive: false olursa birden fazla bağlantıdan erişilebilir


//Queue'ya mesaj gönderme
//RabbitMQ mesajları byte array olarak alır.Haliyle string mesajı byte array'e çevirip göndermemiz gerekiyor.

//byte[] message=Encoding.UTF8.GetBytes("Merhaba RabbitMQ");
//channel.BasicPublish(exchange:"" , routingKey:"example-queue", basicProperties:null, body:message);//exchange: mesajın hangi exchange'e gönderileceğini belirtir. routingKey: hangi queue'ya gönderileceğini belirtir. basicProperties: mesajın özelliklerini belirtir. body: mesajın içeriğini belirtir. 

for (int i = 0; i < 100; i++)
{
    Task.Delay(500).Wait();
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba RabbitMQ {i}");
    channel.BasicPublish(exchange: "", routingKey: "example-queue", basicProperties: null, body: message);
    Console.WriteLine($"Mesaj gönderildi: {i}");
}
Console.Read();

