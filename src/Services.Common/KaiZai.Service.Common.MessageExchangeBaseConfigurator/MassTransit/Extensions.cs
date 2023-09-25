using System.Reflection;
using GreenPipes;
using KaiZai.Service.Common.MessageExchangeBaseConfigurator.Settings;
using MassTransit;
using MassTransit.Definition;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;

namespace KaiZai.Service.Common.MessageExchangeBaseConfigurator.MassTransit;

public static class Extensions
{
    /// <summary>
    /// Adds MassTransit and its dependencies to the collection with some predefined settings.
    ///</summary>
    /// <remarks> 
    /// Default settings defined for RabbitMQ: Host, ConfigureEndpoints, UseMessageRetry. 
    /// Important! Do not forget to configure ServiceSettings and RabbitMQSettings sections in your project.
    /// </remarks>
    /// <param name="collection"></param>
    /// <param name="serviceSettings">Settings with service name</param>
    /// <param name="rabbitMQSettings">Settings for creating a connection to RabbitMQ</param>
    /// <param name="rabbitMqAdditionalConfigurations">The configuration callback for additional settings in RabbitMQ.</param>
    public static IServiceCollection AddMassTransitWithBaseSetUp(this IServiceCollection collection,
        ServiceSettings serviceSettings, 
        RabbitMQSettings rabbitMQSettings,
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator> rabbitMqAdditionalConfigurations = null)
    {
        collection.AddMassTransit(configure => 
        {
            configure.AddConsumers(Assembly.GetEntryAssembly());
            configure.UsingRabbitMq((context, configurator) => 
            {
                if (serviceSettings == null)
                {
                    throw new InvalidOperationException("'ServiceSettings' section is missing or empty in the configuration.");
                }
                if (rabbitMQSettings == null)
                {
                    throw new InvalidOperationException("'RabbitMQSettings' section is missing or empty in the configuration.");
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

        collection.AddMassTransitHostedService();

        return collection;
    }
}