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
    /// Adds MassTransit configuration with a base setup to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="collection">The <see cref="IServiceCollection"/> to which MassTransit configuration will be added.</param>
    /// <param name="serviceSettings">The settings related to the microservice.</param>
    /// <param name="rabbitMQSettings">The settings related to the RabbitMQ message broker.</param>
    /// <param name="rabbitMqAdditionalConfigurations">An optional action for additional MassTransit and RabbitMQ configurations.</param>
    /// <param name="assembliesConsumers">An array of assemblies containing MassTransit consumers. 
    /// If not provided, the calling assembly will be used.
    /// </param>
    /// <returns>The modified <see cref="IServiceCollection"/> with MassTransit configuration.</returns>
    public static IServiceCollection AddMassTransitCoreSetUp(this IServiceCollection collection,
        ServiceSettings serviceSettings, 
        RabbitMQSettings rabbitMQSettings,
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? rabbitMqAdditionalConfigurations = null,
        params Assembly[]? assembliesConsumers)
    {
        collection.AddMassTransit(configure => 
        {
            assembliesConsumers = assembliesConsumers ?? new Assembly[] { Assembly.GetCallingAssembly() };
            configure.AddConsumers(assemblies: assembliesConsumers);
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