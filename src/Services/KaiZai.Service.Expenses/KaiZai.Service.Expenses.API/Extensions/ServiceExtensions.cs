using System.Reflection;
using KaiZai.Service.Common.MessageExchangeBaseConfigurator.MassTransit;
using KaiZai.Service.Common.MessageExchangeBaseConfigurator.Settings;
using KaiZai.Service.Common.MongoDataAccessRepository;
using KaiZai.Service.Common.MongoDataAccessRepository.Settings;
using KaiZai.Service.Expenses.BAL.Services;
using KaiZai.Service.Expenses.CL.Clients;
using KaiZai.Service.Expenses.CL.Filters;
using KaiZai.Service.Expenses.DAL.Repositories;
using MassTransit;
using MassTransit.RabbitMqTransport;
using ServiceSettingsForMessageExchanging = KaiZai.Service.Common.MessageExchangeBaseConfigurator.Settings.ServiceSettings;
using ServiceSettingsForMongoDatabase = KaiZai.Service.Common.MongoDataAccessRepository.Settings.ServiceSettings;

namespace KaiZai.Service.Expenses.API.Extensions;

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
        var assembly = Assembly.GetAssembly(typeof(CategoriesClient));
        var serviceSettings = configuration.GetSection(ServiceSettingsSection).Get<ServiceSettingsForMessageExchanging>();
        var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();

        var rabbitMqAdditionalConfigurations = 
            (IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator) =>
            {
                configurator.UseConsumeFilter(typeof(ExpenseCategoriesConsumersFilter<>), context);
            };

        return services.AddMassTransitCoreSetUp(serviceSettings, rabbitMQSettings, rabbitMqAdditionalConfigurations, assembly);
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryConsumersService, CategoryConsumersService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        return services;
    }
}