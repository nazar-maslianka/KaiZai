using KaiZai.Service.Categories.API.Data.Entities;

namespace KaiZai.Service.Categories.API.Data.Repositories;

public sealed class CategoriesRepository : MongoRepository<Category>, ICategoriesRepository
{
    public CategoriesRepository(IMongoDatabase mongoDatabase, string collectionName = $"{nameof(KaiZai.Service.Categories)}") 
        : base(mongoDatabase, collectionName)
    {
    }
}