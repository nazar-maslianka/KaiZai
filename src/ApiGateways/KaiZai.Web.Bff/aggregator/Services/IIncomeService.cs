using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.WellKnownTypes;
using KaiZai.Web.HttpAggregator.Core;
using KaiZai.Web.HttpAggregator.Models;

namespace KaiZai.Web.HttpAggregator.Services;

public interface IIncomeService
{
    public Task<Empty> AddIncome(AddUpdateIncomeRequest request);

    public Task UpdateIncome(AddUpdateIncomeRequest request);

    public Task DeleteIncome(Guid incomeId);

    public Task<IncomeDataItem> GetIncomeById(Guid incomeId);

    public Task<PagedDataItemsList<IncomeDataItem>> GetPagedIncomes(
        GetPagedIncomesRequest request);
}