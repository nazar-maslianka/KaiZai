using MongoDB.Driver;

namespace KaiZai.Service.Incomes.DAL.Repositories;

public sealed class IncomeRepository : MongoRepository<Income>, IIncomeRepository
{
    public IncomeRepository(IMongoDatabase database, string collectionName = nameof(KaiZai.Service.Incomes)) 
        : base(database, collectionName)
    {
    }
}