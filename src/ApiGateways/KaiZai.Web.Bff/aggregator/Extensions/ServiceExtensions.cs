using GrpcIncomes;
using KaiZai.Web.HttpAggregator.Config;
using KaiZai.Web.HttpAggregator.Services;
using Microsoft.Extensions.Options;

namespace KaiZai.Web.HttpAggregator.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services) 
    {
        services.AddScoped<IIncomeService, IncomeService>();

        services.AddGrpcClient<IncomesGrpc.IncomesGrpcClient>((services, options) =>
        {
            var incomesApi = services.GetRequiredService<IOptions<UrlsConfig>>().Value.GrpcIncomes;
            options.Address = new Uri(incomesApi);
        });

        return services;

    } 
    
}