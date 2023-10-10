using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaiZai.Common.Types;
using KaiZai.Service.Common.MongoDataAccessRepository.Core;

namespace KaiZai.Services.Incomes.BAL.DTOs;

public record CategoryDTO(Guid Id, string Name, CategoryType CategoryType);
public record CategoryShortDTO(Guid Id, string Name);
public record IncomeShortDTO(Guid Id, CategoryShortDTO Category, DateTimeOffset IncomeDate, decimal Amount);
public record IncomeDTO(Guid Id, Guid ProfileId, CategoryDTO Category, DateTimeOffset IncomeDate, string Description, decimal Amount);