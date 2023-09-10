using KaiZai.Service.Categories.API.Data.Entities;

namespace KaiZai.Service.Categories.API.Data.Repositories;

public sealed class CategoryRepository : MongoRepository<Category>, ICategoryRepository
{
    public CategoryRepository(IMongoDatabase mongoDatabase, string collectionName = $"{nameof(KaiZai.Service.Categories)}") 
        : base(mongoDatabase, collectionName)
    {
    }
}