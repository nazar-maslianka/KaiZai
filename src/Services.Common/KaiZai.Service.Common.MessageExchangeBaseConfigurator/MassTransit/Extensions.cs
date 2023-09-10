using System.Reflection;
using GreenPipes;
using KaiZai.Service.Common.MessageExchangeBaseConfigurator.Settings;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KaiZai.Service.Common.MessageExchangeBaseConfigurator.MassTransit;

public static class Extensions
{
    /// <summary>
    /// An extension that configures messaging in RabbitMQ via MassTransit.
    /// Important! Do not forget to configure ServiceSettings and RabbitMQSettings sections in launch.json in your project.
    ///</summary>
    public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
    {
        services.AddMassTransit(configure => 
        {
            configure.AddConsumers(Assembly.GetEntryAssembly());
            configure.UsingRabbitMq((context, configurator) => 
            {
                var configuration = context.GetService<IConfiguration>();
                var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                
                configurator.Host(rabbitMQSettings.Host);
                configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
                configurator.UseMessageRetry(retryConfigurator => 
                {
                    retryConfigurator.Interval(3, TimeSpan.FromSeconds(3));
                });
            });
        });

        services.AddMassTransitHostedService();

        return services;
    }
}