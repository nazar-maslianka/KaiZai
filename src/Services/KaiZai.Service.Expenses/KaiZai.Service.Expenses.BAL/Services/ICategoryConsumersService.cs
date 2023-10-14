using KaiZai.Service.Categories.Contracts;

namespace KaiZai.Service.Expenses.BAL.Services;

public interface ICategoryConsumersService
{
    Task<Result> CreateCategoryAsync(CategoryCreated categoryCreated);
    Task<Result> UpdateCategoryAsync(CategoryUpdated categoryUpdated);
    Task<Result> DeleteCategoryAsync(CategoryDeleted categoryDeleted);
}