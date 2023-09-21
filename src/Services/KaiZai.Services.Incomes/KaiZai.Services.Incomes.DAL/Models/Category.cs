using KaiZai.Common.Types;
using KaiZai.MongoDataAccessAbstraction.Repository;

namespace KaiZai.Services.Incomes.DAL.Models;

public sealed class Category : IEntity
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; }
}