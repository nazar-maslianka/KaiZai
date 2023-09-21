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
            Name = categoryDTO.Name,
            ProfileId = categoryDTO.ProfileId,
            CategoryType = categoryDTO.CategoryType
        };
    }
}