using MongoDB.Driver;
using KaiZai.Service.Incomes.DAL.Contracts;

namespace KaiZai.Service.Incomes.DAL.Repositories;

public sealed class CategoryRepository : MongoRepository<Category>, ICategoryRepository
{
    public CategoryRepository(IMongoDatabase database, string collectionName = nameof(Models.Category)) 
        : base(database, collectionName)
    {
    }
}