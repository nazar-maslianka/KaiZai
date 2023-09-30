using KaiZai.Service.Categories.Contracts;
using KaiZai.Services.Incomes.BAL.Core;

namespace KaiZai.Services.Incomes.BAL.Services;

public interface ICategoryConsumersService
{
    Task CreateCategoryAsync(CategoryCreated categoryCreated);
    Task UpdateCategoryAsync(CategoryUpdated categoryUpdated);
    Task DeleteCategoryAsync(CategoryDeleted categoryDeleted);
}