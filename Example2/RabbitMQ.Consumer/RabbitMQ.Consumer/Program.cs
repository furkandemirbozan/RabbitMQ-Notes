using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;



//Bağlantı oluşturma

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://lrwxleat:Y-P_U-BqIJLnMwkmq34PvtzucOCFGLkN@toad.rmq.cloudamqp.com/lrwxleat");//coludamqp bağlantı adresi


//Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


//Queue(Kuyruk) Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false,durable:true);//publisher ile aynı tanımlanmalıdır

//Queue(Kuyruk) Mesaj Okuma
EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);//autoAck i false yapıp RabbitMq nun  mesajın silinmesini engelliyoruz

channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);//prefetchSize ve prefetchCount 1 yaparak sadece 1 mesajı işleyeceğimizi belirtiyoruz.global false yaparak sadece bu kanalda geçerli olmasını sağlıyoruz


consumer.Received += (sender, e) =>
{
    
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    channel.BasicAck(deliveryTag: e.DeliveryTag,multiple: false);//eğer işlem başarılı ise mesajı silmesini belirtiyoruz.multiple:false tek bir mesajı siler
};
Console.Read();