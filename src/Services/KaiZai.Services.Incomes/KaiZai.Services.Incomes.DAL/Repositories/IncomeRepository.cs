using MongoDB.Driver;

namespace KaiZai.Services.Incomes.DAL.Repositories;

public sealed class IncomeRepository : MongoRepository<Income>, IIncomeRepository
{
    public IncomeRepository(IMongoDatabase database, string collectionName = nameof(KaiZai.Services.Incomes)) 
        : base(database, collectionName)
    {
    }
}