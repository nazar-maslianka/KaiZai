using KaiZai.Common.Types;

namespace KaiZai.Service.Categories.Contracts;

public sealed record CategoryCreated : IBaseCategoryContract
{
    public CategoryCreated(Guid id, Guid profileId, string name, CategoryType categoryType)
    {
        Id = id;
        ProfileId = profileId;
        Name = name;
        CategoryType = categoryType;
    }
    
    public Guid Id { get; init; }
    public Guid ProfileId { get; init; }
    public string Name { get; init; }
    public CategoryType CategoryType { get; init; }
}
public sealed record CategoryUpdated : IBaseCategoryContract
{
    public CategoryUpdated(Guid id, Guid profileId, string name, CategoryType categoryType)
    {
        Id = id;
        ProfileId = profileId;
        Name = name;
        CategoryType = categoryType;
    }
    
    public Guid Id { get; init; }
    public Guid ProfileId { get; init; }
    public string Name { get; init; }
    public CategoryType CategoryType { get; init; }
}
public sealed record CategoryDeleted : IBaseCategoryContract
{
    public CategoryDeleted(Guid id, Guid profileId, CategoryType categoryType)
    {
        Id = id;
        ProfileId = profileId;
        CategoryType = categoryType;
    }

    public Guid Id { get; init; }
    public Guid ProfileId { get; init; }
    public CategoryType CategoryType { get; init; }
}