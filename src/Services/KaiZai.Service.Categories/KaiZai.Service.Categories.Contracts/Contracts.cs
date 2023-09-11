using KaiZai.Common.Types;

namespace KaiZai.Service.Categories.Contracts;

public record CategoryCreated(Guid Id, Guid ProfileId, string Name, CategoryType CategoryType);
//TODO: later
//public record CatalogItemUpdated(Guid ItemId, string Name, string Description);
//public record CatalogItemDeleted(Guid ItemId);