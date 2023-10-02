using KaiZai.Service.Categories.API.Data.Repositories;
using KaiZai.Service.Common.MessageExchangeBaseConfigurator.MassTransit;
using KaiZai.Service.Common.MessageExchangeBaseConfigurator.Settings;
using KaiZai.Service.Common.MongoDataAccessRepository.Settings;
using ServiceSettingsForMessageExchanging = KaiZai.Service.Common.MessageExchangeBaseConfigurator.Settings.ServiceSettings;
using ServiceSettingsForMongoDatabase = KaiZai.Service.Common.MongoDataAccessRepository.Settings.ServiceSettings;

namespace KaiZai.Service.Categories.API.Extensions;

public static class ServiceExtensions
{
    private static readonly string ServiceSettingsSection = "ServiceSettings";
    public static IServiceCollection ConfigureMongoDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceSettings = configuration.GetSection(ServiceSettingsSection).Get<ServiceSettingsForMongoDatabase>();
        var mongoConnectionSettings = configuration.GetSection(nameof(MongoConnectionSettings)).Get<MongoConnectionSettings>();
        return services.AddMongoDatabase(serviceSettings, mongoConnectionSettings);
    }
       public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceSettings = configuration.GetSection(ServiceSettingsSection).Get<ServiceSettingsForMessageExchanging>();
        var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
        return services.AddMassTransitCoreSetUp(serviceSettings, rabbitMQSettings);
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        return services;
    }

    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidationFilterAttribute>();
        return services;
    }
}