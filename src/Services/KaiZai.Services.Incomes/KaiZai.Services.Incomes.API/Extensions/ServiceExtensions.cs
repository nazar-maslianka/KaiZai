using KaiZai.Service.Common.MessageExchangeBaseConfigurator.MassTransit;
using KaiZai.Service.Common.MessageExchangeBaseConfigurator.Settings;
using ServiceSettingsForMessageExchangeConfigurator = KaiZai.Service.Common.MessageExchangeBaseConfigurator.Settings.ServiceSettings;

namespace KaiZai.Services.Incomes.API.Extensions;

public static class ServiceExtensions
{
    private static readonly string ServiceSettingsSection = "ServiceSettings";
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceSettings = configuration.GetSection(ServiceSettingsSection).Get<ServiceSettingsForMessageExchangeConfigurator>();
        var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
        return services.AddMassTransitWithBaseSetUp(serviceSettings, rabbitMQSettings);
    }
    
}