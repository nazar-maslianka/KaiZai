using KaiZai.Common.Types;

namespace KaiZai.Service.Categories.Contracts;

public sealed record CategoryCreated : IBaseCategoryContract
{
    public Guid Id { get; init; }
    public Guid ProfileId { get; init; }
    public string Name { get; init; }
    public CategoryType CategoryType { get; init; }
}
public sealed record CategoryUpdated : IBaseCategoryContract
{
    public Guid Id { get; init; }
    public Guid ProfileId { get; init; }
    public string Name { get; init; }
    public CategoryType CategoryType { get; init; }
}
public sealed record CategoryDeleted : IBaseCategoryContract
{
    public Guid Id { get; init; }
    public Guid ProfileId { get; init; }
    public CategoryType CategoryType { get; init; }
}