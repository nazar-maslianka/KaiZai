using KaiZai.MongoDataAccessAbstraction.Repository;
using KaiZai.Services.Incomes.DAL.Models;

namespace KaiZai.Services.Incomes.DAL.Repositories;

public interface IIncomesRepository : IMongoRepository<Income>
{
}