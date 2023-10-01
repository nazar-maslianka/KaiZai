using KaiZai.Services.Incomes.BAL.DTOs;
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
}