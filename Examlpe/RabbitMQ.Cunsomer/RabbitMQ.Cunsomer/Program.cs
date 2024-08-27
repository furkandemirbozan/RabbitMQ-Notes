using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;



//Bağlantı oluşturma

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://dozwyqqg:dmvpzcZvndpTgQ-jTzp3AuaN1321JCu_@toad.rmq.cloudamqp.com/dozwyqqg");


//Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


//Queue(Kuyruk) Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);//publisher ile aynı tanımlanmalıdır

//Queue(Kuyruk) Mesaj Okuma
EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: "example-queue", false, consumer);//example-queue isimli kuyruğa ne zaman mesaj gelirse onu conume (tüket) et

consumer.Received += (sender, e) =>
{
    //Kuyruğa gelen mesajın işlendiği yer burasıdır
    //e.Body mesajın verisini getirir
    //e.Body veya e.Body.ToArray() ile mesajın verisini alabiliriz
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};
Console.Read();