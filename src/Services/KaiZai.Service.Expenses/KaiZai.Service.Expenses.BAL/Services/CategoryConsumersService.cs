using KaiZai.Service.Categories.Contracts;

namespace KaiZai.Service.Expenses.BAL.Services;

public sealed class CategoryConsumersService : ICategoryConsumersService
{
    private readonly ICategoryRepository _categoryRepository;
    
    public CategoryConsumersService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> CreateCategoryAsync(CategoryCreated categoryCreated)
    {
        if (categoryCreated == null)
        {
            throw new ArgumentNullException(nameof(categoryCreated));
        }

        var existingCategory = await _categoryRepository.GetOneAsync(categoryCreated.Id);
        if (existingCategory != null)
        {
            return Result.Failure($"Category with ID: {categoryCreated.Id} already present in database");
        }

        var category = categoryCreated.ToCategory();
        await _categoryRepository.CreateAsync(category);
        return Result.Success();
    }

    public async Task<Result> UpdateCategoryAsync(CategoryUpdated categoryUpdated)
    {
        if (categoryUpdated == null)
        {
            throw new ArgumentNullException(nameof(categoryUpdated));
        }

        var existingCategory = await _categoryRepository.GetOneAsync(categoryUpdated.Id);
        if (existingCategory == null)
        {
            await _categoryRepository.CreateAsync(categoryUpdated.ToCategory());
            return Result.Success();
        }

        existingCategory.Name = categoryUpdated.Name;
        existingCategory.CategoryType = categoryUpdated.CategoryType;

        //TODO: think about updating only some fields by filter!!!
        await _categoryRepository.UpdateAsync(existingCategory);
        return Result.Success();
    }

    public async Task<Result> DeleteCategoryAsync(CategoryDeleted categoryDeleted)
    {
        if (categoryDeleted == null)
        {
            throw new ArgumentNullException(nameof(categoryDeleted));
        }

        var existingCategory = await _categoryRepository.GetOneAsync(categoryDeleted.Id);
        if (existingCategory == null)
        {
            return Result.Failure($"Category with ID: {categoryDeleted.Id} not present in database");
        }
        
        await _categoryRepository.RemoveAsync(categoryDeleted.Id);
        return Result.Success();
    }
}