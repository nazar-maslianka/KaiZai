using Polly;
using Polly.Timeout;

namespace KaiZai.Services.Incomes.API.Clients;

public sealed class CategoriesClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CategoriesClient> _logger;
    public CategoriesClient(HttpClient httpClient,
        ILogger<CategoriesClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<CategoryDTO>> GetCategoriesAsync(Guid profileId)
    {
        var items = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<CategoryDTO>>($"api/profile/{profileId}/[controller]");
        return items;
    }

    //TODO:later change logging provider to serilog !!!
    
}