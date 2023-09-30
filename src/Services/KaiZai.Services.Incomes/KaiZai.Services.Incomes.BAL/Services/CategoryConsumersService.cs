using KaiZai.Service.Categories.Contracts;
using KaiZai.Services.Incomes.BAL.Core;
using KaiZai.Services.Incomes.BAL.DTOs;
using KaiZai.Services.Incomes.DAL.Models;
using KaiZai.Services.Incomes.DAL.Repositories;
using Microsoft.Extensions.Logging;

namespace KaiZai.Services.Incomes.BAL.Services;

public sealed class CategoryConsumersService : ICategoryConsumersService
{
    private readonly ILogger<CategoryConsumersService> _logger;
    private readonly ICategoryRepository _categoryRepository;
    
    public CategoryConsumersService(ILogger<CategoryConsumersService> logger,
        ICategoryRepository categoryRepository)
    {
        _logger = logger;
        _categoryRepository = categoryRepository;
    }

    public async Task CreateCategoryAsync(CategoryCreated categoryCreated)
    {
        if (categoryCreated == null)
        {
            throw new ArgumentNullException(nameof(categoryCreated));
        }

        var existingCategory = await _categoryRepository.GetAsync(categoryCreated.Id);
        if (existingCategory != null)
        {
            _logger.LogInformation($"Category with ID: {categoryCreated.Id} already present in database");
            return;
        }

        var category = categoryCreated.ToCategory();
        await _categoryRepository.CreateAsync(category);
    }

    public async Task UpdateCategoryAsync(CategoryUpdated categoryUpdated)
    {
        if (categoryUpdated == null)
        {
            throw new ArgumentNullException(nameof(categoryUpdated));
        }

        var existingCategory = await _categoryRepository.GetAsync(categoryUpdated.Id);
        if (existingCategory == null)
        {
            await _categoryRepository.CreateAsync(categoryUpdated.ToCategory());
            return;
        }

        existingCategory.Name = categoryUpdated.Name;
        existingCategory.CategoryType = categoryUpdated.CategoryType;

        //TODO: add features for updating only fields by some filter!!!
        await _categoryRepository.UpdateAsync(existingCategory);
    }

    public async Task DeleteCategoryAsync(CategoryDeleted categoryDeleted)
    {
        if (categoryDeleted == null)
        {
            throw new ArgumentNullException(nameof(categoryDeleted));
        }

        var existingCategory = await _categoryRepository.GetAsync(categoryDeleted.Id);
        if (existingCategory == null)
        {
            _logger.LogInformation($"Category with ID: {categoryDeleted.Id} not present in database");
            return;
        }
        
        await _categoryRepository.RemoveAsync(categoryDeleted.Id);
    }
}