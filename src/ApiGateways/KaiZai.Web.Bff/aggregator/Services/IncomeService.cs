using KaiZai.Web.HttpAggregator.Core;
using KaiZai.Web.HttpAggregator.Models;
using GrpcIncomes;
using Google.Protobuf.WellKnownTypes;
using KaiZai.Web.HttpAggregator.Grpc;

namespace KaiZai.Web.HttpAggregator.Services;

public sealed class IncomeService : IIncomeService
{
    private readonly ILogger<IncomeService> _logger;
    private readonly IncomesGrpc.IncomesGrpcClient _incomesClient;

    public IncomeService(IncomesGrpc.IncomesGrpcClient incomesClient,
        ILogger<IncomeService> logger)
    {
        _incomesClient = incomesClient;
        _logger = logger;
    }

    public async Task<IncomeDataItem> GetIncomeById(Guid incomeId)
    {
         _logger.LogDebug("grpc client created, request = {@id}", incomeId);
        var grpcIncomeDTO = await _incomesClient.GetIncomeByIdAsync(new GetIncomeByIdRequest { IncomeId = incomeId });
        _logger.LogDebug("grpc response {@response}", grpcIncomeDTO);
        
        return MapToIncomeDataItem(grpcIncomeDTO);
    }

    public async Task<PagedDataItemsList<IncomeDataItem>> GetPagedIncomes(GetPagedIncomesRequest request)
    {
        var grpcGetIncomesAggregatedByPageRequest = MapToGrpcGetIncomesAggregatedByPageRequest(request);
        var grpcIncomesAggregatedByPageResult = await _incomesClient.GetIncomesAggregatedByPageAsync(grpcGetIncomesAggregatedByPageRequest);

        var incomeDataItems = grpcIncomesAggregatedByPageResult.PagedList.Items
            .Select(x => MapToIncomeDataItem(MessageConverters.DeserializeAny<IncomeDTO>(x)));

        return new PagedDataItemsList<IncomeDataItem>(incomeDataItems ?? Enumerable.Empty<IncomeDataItem>(), 
            grpcIncomesAggregatedByPageResult.PagedList.Metadata.TotalCount,
            grpcIncomesAggregatedByPageResult.PagedList.Metadata.CurrentPage,
            grpcIncomesAggregatedByPageResult.PagedList.Metadata.PageSize);
    }

    public async Task<Empty> AddIncome(AddUpdateIncomeRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException($"Invalid parameter {nameof(request)}");
        }

        var response = await _incomesClient.AddIncomeAsync(new AddIncomeRequest
        {
            ProfileId = request.ProfileId,
            AddIncomeDTO = new AddUpdateIncomeDTO
            {
                CategoryId = request.CategoryId,
                Description = request.Description,
                Amount = request.Amount
            }
        });

        return response;
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

    private GetIncomesAggregatedByPageRequest MapToGrpcGetIncomesAggregatedByPageRequest(GetPagedIncomesRequest getPagedIncomesRequest) 
    {
        if (getPagedIncomesRequest == null)
        {
            return null;
        }

        return new GetIncomesAggregatedByPageRequest
        {
            ProfileId = getPagedIncomesRequest.ProfileId,
            PagingParams = new GrpcIncomes.PagingParams
            {
                PageNumber = getPagedIncomesRequest.PagingParams.PageNumber,
                PageSize = getPagedIncomesRequest.PagingParams.PageSize,
            },
            FilteringParams = getPagedIncomesRequest.FilteringParams != null
                ? new GrpcIncomes.FilteringParams
                {
                    StartDate = Timestamp.FromDateTimeOffset(getPagedIncomesRequest.FilteringParams.StartDate),
                    EndDate = Timestamp.FromDateTimeOffset(getPagedIncomesRequest.FilteringParams.EndDate)
                } 
                : null
            // getPagedIncomesRequest.FilteringParams
        };
    }
    
}