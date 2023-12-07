using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaiZai.Service.Incomes.DAL.Contracts;

public interface ICategoryRepository : IMongoRepository<Category>
{
    
}