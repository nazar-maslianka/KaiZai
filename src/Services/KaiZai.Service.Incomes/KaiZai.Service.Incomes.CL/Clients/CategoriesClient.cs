using System.Net.Http.Json;
using KaiZai.Service.Incomes.BAL.DTOs;

namespace KaiZai.Service.Incomes.CL.Clients;

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