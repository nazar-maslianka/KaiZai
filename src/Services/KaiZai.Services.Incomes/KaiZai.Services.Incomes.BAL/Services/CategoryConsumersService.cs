using KaiZai.Service.Categories.Contracts;
using KaiZai.Services.Incomes.BAL.Core;
using KaiZai.Services.Incomes.BAL.DTOs;
using KaiZai.Services.Incomes.DAL.Models;
using KaiZai.Services.Incomes.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace KaiZai.Services.Incomes.BAL.Services;

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

        var existingCategory = await _categoryRepository.GetAsync(categoryCreated.Id);
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

        var existingCategory = await _categoryRepository.GetAsync(categoryUpdated.Id);
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

        var existingCategory = await _categoryRepository.GetAsync(categoryDeleted.Id);
        if (existingCategory == null)
        {
            return Result.Failure($"Category with ID: {categoryDeleted.Id} not present in database");
        }
        
        await _categoryRepository.RemoveAsync(categoryDeleted.Id);
        return Result.Success();
    }
}