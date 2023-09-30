using System.Linq.Expressions;
using KaiZai.Services.Incomes.BAL.Core;
using KaiZai.Services.Incomes.BAL.DTOs;
using KaiZai.Services.Incomes.DAL.Models;
using KaiZai.Services.Incomes.DAL.Repositories;
using MongoDB.Driver;

namespace KaiZai.Services.Incomes.BAL.Services;

public sealed class IncomeService 
{
    private readonly IIncomeRepository _incomesRepository;

    public IncomeService(IIncomeRepository incomesRepository)
    {
        _incomesRepository = incomesRepository;
    }

    //public async Task<Result<IReadOnlyList<IncomeShortDTO>>> GetIncomesAggregatedByPageAsync(Guid profileId,
    //    PagingParams pagingParams,
    //    FilteringParams filteringParams) 
    //{
    //    Expression<Func<Income, bool>>? filterDefinition = income => 
    //        income.ProfileId.Equals(profileId)
    //        && income.IncomeDate >= (filteringParams.StartDate ?? DateTimeOffset.UtcNow.AddDays(-30))
    //        && income.IncomeDate <= (filteringParams.StartDate ?? DateTimeOffset.UtcNow);
//
    //    FieldDefinition<Income> sortDefinitionField = nameof(Income.IncomeDate);
    //    var aggregatedResult = await _incomesRepository.GetAggregateByPageAsync(filterDefinition, sortDefinitionField, pagingParams.PageNumber, pagingParams.PageSize, pagingParams.PageSize, paging);
    //    return Result<IReadOnlyList<IncomeShortDTO>>.Success();
    //}
}