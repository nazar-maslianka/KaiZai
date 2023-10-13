namespace KaiZai.Service.Expenses.DAL.Repositories;

public sealed class CategoryRepository : MongoRepository<Category>, ICategoryRepository
{
    public CategoryRepository(IMongoDatabase mongoDatabase, string collectionName = nameof(Models.Category)) 
        : base(mongoDatabase, collectionName)
    {
    }
}