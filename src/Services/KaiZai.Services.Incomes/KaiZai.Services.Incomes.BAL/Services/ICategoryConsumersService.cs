using KaiZai.Service.Categories.Contracts;
using KaiZai.Services.Incomes.BAL.Core;

namespace KaiZai.Services.Incomes.BAL.Services;

public interface ICategoryConsumersService
{
    Task<Result> CreateCategoryAsync(CategoryCreated categoryCreated);
    Task<Result> UpdateCategoryAsync(CategoryUpdated categoryUpdated);
    Task<Result> DeleteCategoryAsync(CategoryDeleted categoryDeleted);
}