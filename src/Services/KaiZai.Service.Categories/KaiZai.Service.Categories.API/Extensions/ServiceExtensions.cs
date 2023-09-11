using KaiZai.Service.Categories.API.Data.Repositories;

namespace KaiZai.Service.Categories.API.Extensions;

public static class ServiceExtensions
{
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