using KaiZai.Service.Categories.API.Data.Entities;

namespace KaiZai.Service.Categories.API.Data.Repositories;

public interface ICategoryRepository : IMongoRepository<Category>
{
}