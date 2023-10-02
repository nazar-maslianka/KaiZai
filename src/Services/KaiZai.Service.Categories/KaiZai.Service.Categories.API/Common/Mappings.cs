using KaiZai.Service.Categories.API.Data.Entities;
using KaiZai.Service.Categories.API.DTOs;

namespace KaiZai.Service.Categories.API.Common;

public static class Mappings
{
    public static CategoryDTO ToCategoryDTO(this Category categoryItem)
    {
        return new CategoryDTO
        {
            Id = categoryItem.Id,
            Name = categoryItem.Name,
            ProfileId = categoryItem.ProfileId,
            CategoryType = categoryItem.CategoryType
        };
    }

    public static Category ToCategory(this CreateUpdateCategoryDTO createCategoryDTO, Guid profileId)
    {
        return new Category
        {
            ProfileId = profileId,
            Name = createCategoryDTO.Name,
            CategoryType = createCategoryDTO.CategoryType
        };
    }

     public static Category ToCategory(this CreateUpdateCategoryDTO createCategoryDTO, Guid profileId, Guid id)
    {
        return new Category
        {
            Id = id,
            ProfileId = profileId,
            Name = createCategoryDTO.Name,
            CategoryType = createCategoryDTO.CategoryType
        };
    }
}