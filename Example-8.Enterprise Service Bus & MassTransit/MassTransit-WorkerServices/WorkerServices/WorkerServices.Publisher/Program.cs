



using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkerServices.Publisher.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((services) =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.UsingRabbitMq((context,
                _configurator) =>
            {
                _configurator.Host("amqps://htvazwsp:vdrsshC7zjCHReck1B3jHzJuX66eIDXw@moose.rmq.cloudamqp.com/htvazwsp");
            });
        });

        services.AddHostedService<PublishMessageServices>(provider =>
        {
            using IServiceScope scope = provider.CreateScope();
            IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            return new(publishEndpoint);
        });

    })
    .Build();


host.Run();