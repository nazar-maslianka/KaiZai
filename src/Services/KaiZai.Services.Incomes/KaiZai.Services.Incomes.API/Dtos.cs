using KaiZai.Common.Types;

namespace KaiZai.Services.Incomes.API;

public sealed record CategoryDTO
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; } 
}