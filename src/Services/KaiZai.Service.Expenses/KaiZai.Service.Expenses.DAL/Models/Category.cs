using KaiZai.Common.Types;

namespace KaiZai.Service.Expenses.DAL.Models;

public sealed class Category : IEntity
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; }
}