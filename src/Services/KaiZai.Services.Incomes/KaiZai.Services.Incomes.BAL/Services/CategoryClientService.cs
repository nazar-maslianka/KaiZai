using KaiZai.Services.Incomes.BAL.Core;
using KaiZai.Services.Incomes.BAL.DTOs;
using KaiZai.Services.Incomes.DAL.Models;
using KaiZai.Services.Incomes.DAL.Repositories;

namespace KaiZai.Services.Incomes.BAL.Services;

public class CategoryClientService : ICategoryClientService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryClientService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

     public async Task CreateCategoryAsync(CategoryDTO categoryDTO)
    {
        if (categoryDTO == null)
        {
            throw new ArgumentNullException(nameof(categoryDTO));
        }

        // You can add any validation logic here before creating the category.
        var category = categoryDTO.ToCategory();
        await _categoryRepository.CreateAsync(category);
    }

    public async Task<Category> UpdateCategoryAsync(Guid categoryId, Category updatedCategory)
    {
        if (updatedCategory == null)
        {
            throw new ArgumentNullException(nameof(updatedCategory));
        }

        // You can add validation logic here to ensure the category with the given ID exists.

        var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
        if (existingCategory == null)
        {
            throw new ArgumentException($"Category with ID {categoryId} not found.");
        }

        // Update the properties of the existing category with the values from updatedCategory.
        existingCategory.Name = updatedCategory.Name;
        existingCategory.CategoryType = updatedCategory.CategoryType;

        return await _categoryRepository.UpdateAsync(existingCategory);
    }

    public async Task<bool> DeleteCategoryAsync(Guid categoryId)
    {
        // You can add validation logic here to ensure the category with the given ID exists.

        var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
        if (existingCategory == null)
        {
            return false; // Category not found, return false to indicate failure.
        }

        await _categoryRepository.DeleteAsync(categoryId);
        return true; // Deletion successful.
    }
}