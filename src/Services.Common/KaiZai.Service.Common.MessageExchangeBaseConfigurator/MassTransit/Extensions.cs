using System.Reflection;
using GreenPipes;
using KaiZai.Service.Common.MessageExchangeBaseConfigurator.Settings;
using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KaiZai.Service.Common.MessageExchangeBaseConfigurator.MassTransit;

public static class Extensions
{
    /// <summary>
    /// An extension for configuring MassTransit with default settings.
    /// Default settings defined for RabbitMQ: Host, ConfigureEndpoints, UseMessageRetry.
    /// Important! Do not forget to configure ServiceSettings and RabbitMQSettings sections in launch.json in your project.
    ///</summary>
    /// <param name="rabbitMqAdditionalConfigurations">Parameter for additional settings in RabbitMQ.</param>
    public static IServiceCollection AddMassTransit(this IServiceCollection services,  
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator> rabbitMqAdditionalConfigurations = null)
    {
        services.AddMassTransit(configure => 
        {
            configure.AddConsumers(Assembly.GetEntryAssembly());
            configure.UsingRabbitMq((context, configurator) => 
            {
                var configuration = context.GetService<IConfiguration>();
                var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                if (rabbitMQSettings == null)
                {
                    throw new InvalidOperationException("'RabbitMQSettings' section is missing or empty in the configuration.");
                }
                if (serviceSettings == null)
                {
                    throw new InvalidOperationException("'ServiceSettings' section is missing or empty in the configuration.");
                }
                configurator.Host(rabbitMQSettings.Host);
                configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));
                configurator.UseMessageRetry(retryConfigurator => 
                {
                    retryConfigurator.Interval(3, TimeSpan.FromSeconds(3));
                });
                if (rabbitMqAdditionalConfigurations != null)
                {
                    rabbitMqAdditionalConfigurations(context, configurator);
                }
            });
        });

        services.AddMassTransitHostedService();

        return services;
    }
}