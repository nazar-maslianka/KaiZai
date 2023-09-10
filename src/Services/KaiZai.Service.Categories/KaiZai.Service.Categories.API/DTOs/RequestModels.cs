namespace KaiZai.Service.Categories.API.DTOs;

public sealed record CreateCategoryDTO
{
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; } 
}