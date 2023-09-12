using Polly;
using Polly.Timeout;

namespace KaiZai.Services.Incomes.API.Clients;

public sealed class CategoriesClient
{
    private readonly HttpClient _httpClient;

    public CategoriesClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyCollection<CategoryDTO>> GetCategoriesAsync(Guid profileId)
    {
        var items = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<CategoryDTO>>($"api/profile/{profileId}/[controller]");
        return items;
    }

    //TODO:later change logging provider to serilog !!!
    public static void AddCategoriesClient(IServiceCollection services)
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
    }
}