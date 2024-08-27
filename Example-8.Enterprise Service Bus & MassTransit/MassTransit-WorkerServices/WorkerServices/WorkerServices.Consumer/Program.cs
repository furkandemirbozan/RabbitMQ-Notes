using MassTransit;
using Microsoft.Extensions.Hosting;
using WorkerServices.Consumer.Conusmer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((services) =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<ExampleMessageConsumer>();

            configurator.UsingRabbitMq((context,_configurator) =>
            {
                _configurator.Host("amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp");

                _configurator.ReceiveEndpoint("example-message-queue", e =>
                {
                    e.ConfigureConsumer<ExampleMessageConsumer>(context);
                });
            });
        });
    })
    .Build();

await host.RunAsync();