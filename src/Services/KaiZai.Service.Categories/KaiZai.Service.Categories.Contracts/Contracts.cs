namespace KaiZai.Service.Categories.Contracts;

public record CategoryCreated(Guid Id, Guid ProfileId, string Name);
//public record CatalogItemUpdated(Guid ItemId, string Name, string Description);
//public record CatalogItemDeleted(Guid ItemId);