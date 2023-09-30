using KaiZai.Service.Categories.Contracts;
using KaiZai.Services.Incomes.BAL.DTOs;
using KaiZai.Services.Incomes.DAL.Models;

namespace KaiZai.Services.Incomes.BAL.Core;

public static class Mappings
{
    public static Category ToCategory(this CategoryDTO categoryDTO)
    {
        return new Category
        {
            Id = categoryDTO.Id,
            ProfileId = categoryDTO.ProfileId,
            Name = categoryDTO.Name,
            CategoryType = categoryDTO.CategoryType
        };
    }

    public static Category ToCategory(this CategoryCreated categoryCreated)
    {
        return new Category
        {
            Id = categoryCreated.Id,
            ProfileId = categoryCreated.ProfileId,
            Name = categoryCreated.Name,
            CategoryType = categoryCreated.CategoryType
        };
    }

    public static Category ToCategory(this CategoryUpdated categoryUpdated)
    {
        return new Category
        {
            Id = categoryUpdated.Id,
            ProfileId = categoryUpdated.ProfileId,
            Name = categoryUpdated.Name,
            CategoryType = categoryUpdated.CategoryType
        };
    }
}