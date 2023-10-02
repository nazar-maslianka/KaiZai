using System.ComponentModel.DataAnnotations;

namespace KaiZai.Service.Categories.API.DTOs;

public sealed record CreateUpdateCategoryDTO
{
    [Required]
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; } 
}