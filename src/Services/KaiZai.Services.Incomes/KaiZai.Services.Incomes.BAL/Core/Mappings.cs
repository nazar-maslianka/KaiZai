using System.ComponentModel.DataAnnotations;
using KaiZai.Service.Categories.Contracts;
using KaiZai.Services.Incomes.BAL.DTOs;
using KaiZai.Services.Incomes.DAL.Models;

namespace KaiZai.Services.Incomes.BAL.Core;

public static class Mappings
{
    public static Category ToCategory(this CategoryDTO categoryDTO)
    {
        if (categoryDTO == null)
        {
            throw new ArgumentNullException(nameof(categoryDTO));
        }

        return new Category
        {
            Id = categoryDTO.Id,
            Name = categoryDTO.Name,
            CategoryType = categoryDTO.CategoryType
        };
    }

    public static Category ToCategory(this CategoryCreated categoryCreated)
    {
        if (categoryCreated == null)
        {
            throw new ArgumentNullException(nameof(categoryCreated));
        }

        return new Category
        {
            Id = categoryCreated.Id,
            Name = categoryCreated.Name,
            ProfileId = categoryCreated.ProfileId,
            CategoryType = categoryCreated.CategoryType
        };
    }

    public static Category ToCategory(this CategoryUpdated categoryUpdated)
    {
        if (categoryUpdated == null)
        {
            throw new ArgumentNullException(nameof(categoryUpdated));
        }

        return new Category
        {
            Id = categoryUpdated.Id,
            ProfileId = categoryUpdated.ProfileId,
            Name = categoryUpdated.Name,
            CategoryType = categoryUpdated.CategoryType
        };
    }

    public static IncomeDTO ToIncomeDTO(this Income income, Category category)
    {
        if (income == null)
        {
            throw new ArgumentNullException(nameof(income));
        }

        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        return new IncomeDTO
        (
            income.Id,
            income.ProfileId,
            new CategoryDTO
            (
                category.Id,
                category.Name,
                category.CategoryType
            ),
            income.IncomeDate,
            income.Description,
            income.Amount
        );
    }

    public static IncomeShortDTO ToIncomeShortDTO(this Income income, Category category)
    {
        if (income == null)
        {
            throw new ArgumentNullException(nameof(income));
        }

        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        return new IncomeShortDTO
        (
            income.Id,
            new CategoryShortDTO
            (
                category.Id,
                category.Name
            ),
            income.IncomeDate,
            income.Amount
        );
    }

    public static Income ToIncome(this AddUpdateIncomeDTO addUpdateIncomeDTO, Guid profileId, Guid? incomeId = null)
    {
        if (addUpdateIncomeDTO == null)
        {
            throw new ArgumentNullException(nameof(addUpdateIncomeDTO));
        }

        if (profileId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(profileId));
        }

        var income = new Income
        {
            ProfileId = profileId,
            Amount = addUpdateIncomeDTO.Amount,
            CategoryId = addUpdateIncomeDTO.CategoryId,
            IncomeDate = addUpdateIncomeDTO.IncomeDate,
            Description = addUpdateIncomeDTO.Description
        };

        if (incomeId.HasValue && incomeId.Value != Guid.Empty)
        {
            income.Id = incomeId.Value;
        }

        return income;
    }
}