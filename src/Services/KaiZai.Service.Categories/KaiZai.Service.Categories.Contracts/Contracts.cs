using KaiZai.Common.Types;

namespace KaiZai.Service.Categories.Contracts;

public sealed record CategoryCreated(Guid Id, Guid ProfileId, string Name, CategoryType CategoryType);
public sealed record CategoryUpdated(Guid Id, Guid ProfileId, string Name, CategoryType CategoryType);
public sealed record CategoryDeleted(Guid Id, Guid ProfileId);