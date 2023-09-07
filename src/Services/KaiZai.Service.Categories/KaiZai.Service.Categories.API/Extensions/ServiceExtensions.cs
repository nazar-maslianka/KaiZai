using KaiZai.Service.Categories.API.ActionFilters;
using KaiZai.Service.Categories.API.Data.Repositories;
using KaiZai.Service.Common.MongoDataAccessRepository.Settings;

namespace KaiZai.Service.Categories.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddMongoDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        var mongoConnectionSettings = configuration.GetSection(nameof(MongoConnectionSettings)).Get<MongoConnectionSettings>();
        return services.AddMongoDatabase(mongoConnectionSettings, databaseSettings);
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        return services;
    }

    public static IServiceCollection AddFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidationFilterAttribute>();
        return services;
    }
}