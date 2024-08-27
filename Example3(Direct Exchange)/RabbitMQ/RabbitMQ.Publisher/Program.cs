using RabbitMQ.Client;
using System.Text;








ConnectionFactory factory = new();
factory.Uri = new("amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();





channel.ExchangeDeclare(exchange:"direct-exchange-example", type: ExchangeType.Direct);//Exchange oluşturuldu ve adını verdim her iki yerde de aynı olmalı


while(true)
{
   Console.Write("Message: ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(
        exchange: "direct-exchange-example",
        routingKey: "direct-queue-example",
        body: byteMessage);
}



Console.Read();