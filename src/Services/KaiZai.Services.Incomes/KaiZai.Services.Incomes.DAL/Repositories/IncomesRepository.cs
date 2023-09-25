using KaiZai.MongoDataAccessAbstraction.Repository;
using KaiZai.Services.Incomes.DAL.Models;
using MongoDB.Driver;

namespace KaiZai.Services.Incomes.DAL.Repositories;

public sealed class IncomesRepository : MongoRepository<Income>, IIncomesRepository
{
    public IncomesRepository(IMongoDatabase database, string collectionName = nameof(KaiZai.Services.Incomes)) 
        : base(database, collectionName)
    {
    }
}