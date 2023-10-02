using KaiZai.Service.Common.MessageExchangeBaseConfigurator.MassTransit;
using KaiZai.Service.Common.MessageExchangeBaseConfigurator.Settings;
using KaiZai.Service.Common.MongoDataAccessRepository;
using KaiZai.Service.Common.MongoDataAccessRepository.Settings;
using KaiZai.Services.Incomes.BAL.Services;
using KaiZai.Services.Incomes.DAL.Repositories;
using ServiceSettingsForMessageExchanging = KaiZai.Service.Common.MessageExchangeBaseConfigurator.Settings.ServiceSettings;
using ServiceSettingsForMongoDatabase = KaiZai.Service.Common.MongoDataAccessRepository.Settings.ServiceSettings;

namespace KaiZai.Services.Incomes.API.Extensions;

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
        return services.AddMassTransitWithBaseSetUp(serviceSettings, rabbitMQSettings);
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryConsumersService, CategoryConsumersService>();
        services.AddScoped<IIncomeService, IncomeService>();
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        return services;
    }
}