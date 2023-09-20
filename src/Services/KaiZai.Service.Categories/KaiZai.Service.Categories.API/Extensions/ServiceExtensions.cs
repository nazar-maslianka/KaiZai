using KaiZai.Service.Categories.API.Data.Repositories;
using KaiZai.Service.Common.MongoDataAccessRepository.Settings;

namespace KaiZai.Service.Categories.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddMongoDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
        var mongoConnectionSettings = configuration.GetSection(nameof(MongoConnectionSettings)).Get<MongoConnectionSettings>();
        return services.AddMongoDatabase(serviceSettings, mongoConnectionSettings);
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