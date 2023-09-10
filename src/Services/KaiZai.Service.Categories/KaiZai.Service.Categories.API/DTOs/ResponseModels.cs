namespace KaiZai.Service.Categories.API.DTOs;

public sealed record CategoryDTO : IEntity
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; } 
}