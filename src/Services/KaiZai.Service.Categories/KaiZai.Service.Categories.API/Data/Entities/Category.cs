using KaiZai.Service.Categories.API.Data.Enums;

namespace KaiZai.Service.Categories.API.Data.Entities;

public sealed class Category : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; }
}