using MongoDB.Driver;

namespace KaiZai.Service.Incomes.DAL.Repositories;

public sealed class CategoryRepository : MongoRepository<Category>, ICategoryRepository
{
    public CategoryRepository(IMongoDatabase database, string collectionName = nameof(Models.Category)) 
        : base(database, collectionName)
    {
    }
}