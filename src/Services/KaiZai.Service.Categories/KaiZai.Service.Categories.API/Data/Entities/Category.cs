using System.ComponentModel.DataAnnotations;
using KaiZai.Service.Categories.API.Data.Enums;

namespace KaiZai.Service.Categories.API.Data.Entities;

public sealed class Category : IEntity
{
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; }
}