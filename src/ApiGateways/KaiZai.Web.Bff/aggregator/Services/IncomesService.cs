using System;
using System.Collections.Generic;
using System.Linq;
using KaiZai.Web.HttpAggregator.Core;
using KaiZai.Web.HttpAggregator.Models;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcIncomes;

namespace KaiZai.Web.HttpAggregator.Services;

public sealed class IncomesService : IIncomesService
{
    private readonly ILogger<IncomesService> _logger;
    private readonly IncomesGrpc.IncomesGrpcClient _incomesClient;

    public IncomesService(IncomesGrpc.IncomesGrpcClient incomesClient,
        ILogger<IncomesService> logger)
    {
        _incomesClient = incomesClient;
        _logger = logger;
    }

    public async Task<IncomeDataItem> GetIncomeById(Guid incomeId)
    {
         _logger.LogDebug("grpc client created, request = {@id}", incomeId);
        var response = await _incomesClient.GetIncomeByIdAsync(new GetIncomeByIdRequest { IncomeId = incomeId });
        _logger.LogDebug("grpc response {@response}", response);
        return null;
    }

    public async Task<PagedDataItemsList<IncomeDataItem>> GetPagedIncomes(GetPagedIncomesRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task AddIncome(AddUpdateIncomeRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateIncome(AddUpdateIncomeRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteIncome(Guid incomeId)
    {
        throw new NotImplementedException();
    }


    private IncomeDataItem MapToIncomeDataItem(IncomeDTO incomeGrpcDTO)  
    {
        if (incomeGrpcDTO == null)
        {
            return null;
        }

        return new IncomeDataItem(
            incomeGrpcDTO.Id,
            incomeGrpcDTO.ProfileId,
            incomeGrpcDTO.Category.Id,
            incomeGrpcDTO.IncomeDate.ToDateTimeOffset(),
            incomeGrpcDTO.Description,
            incomeGrpcDTO.Amount
        );
    }
    
}