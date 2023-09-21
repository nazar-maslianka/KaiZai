using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaiZai.Common.Types;
using KaiZai.MongoDataAccessAbstraction.Repository;

namespace KaiZai.Services.Incomes.BAL.DTOs;

public sealed record CategoryDTO : IEntity
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; } 
}