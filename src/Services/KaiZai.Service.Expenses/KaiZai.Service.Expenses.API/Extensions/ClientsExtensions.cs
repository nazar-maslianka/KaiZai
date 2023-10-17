using KaiZai.Service.Expenses.CL.Clients;
using Polly;
using Polly.Timeout;

namespace KaiZai.Service.Expenses.API.Extensions;

public static class ClientsExtensions
{
    public static IServiceCollection AddCategoriesClientHttpHandler(this IServiceCollection services)
    {
        Random jitterer = new Random();
        services.AddHttpClient<CategoriesClient>(client => 
        { 
            client.BaseAddress = new Uri("https://localhost:7237");
        })
        .AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>().WaitAndRetryAsync(
            5,
            retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                + TimeSpan.FromMilliseconds(jitterer.Next(0, 1000)),
            onRetry: (outcome, timespan, retryAttempt) =>
            {
                var serviceProvider = services.BuildServiceProvider();
                serviceProvider.GetService<ILogger<CategoriesClient>>()?
                    .LogWarning($"Delaying for {timespan.TotalSeconds} seconds, then making retry {retryAttempt}");
            }
        ))
        .AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>().CircuitBreakerAsync(
              3,
              TimeSpan.FromSeconds(15),
              onBreak: (outcome, timespan) =>
              {
                  var serviceProvider = services.BuildServiceProvider();
                  serviceProvider.GetService<ILogger<CategoriesClient>>()?
                      .LogWarning($"Opening the circuit for {timespan.TotalSeconds} seconds...");
              },
              onReset: () =>
              {
                  var serviceProvider = services.BuildServiceProvider();
                  serviceProvider.GetService<ILogger<CategoriesClient>>()?
                      .LogWarning($"Closing the circuit...");
              }
        ))
        .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));

        return services;
    }
}