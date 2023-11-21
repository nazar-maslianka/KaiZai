using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using KaiZai.Service.Incomes.API.Grpc;
using KaiZai.Service.Incomes.BAL.Contracts;
using BALayerCore = KaiZai.Service.Incomes.BAL.Core;

namespace GrpcIncomes;

public sealed partial class IncomesGrpcService : IncomesGrpc.IncomesGrpcBase
{
    private readonly ILogger<IncomesGrpcService> _logger;
    private readonly IIncomeService _incomeService;

    public IncomesGrpcService(ILogger<IncomesGrpcService> logger, IIncomeService incomeService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _incomeService = incomeService ?? throw new ArgumentNullException(nameof(incomeService));
    }

    public override async Task<Empty> AddIncome(AddIncomeRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Begin gRPC call IncomesGrpcService.AddIncomeAsync for profile id {Profileid}", request.ProfileId);

        var result = await _incomeService.AddIncomeAsync(
            request.ProfileId,
            request.ToBALayerAddUpdateIncomeDTO());

        if (result.ProcessStatus == BALayerCore.ProcessStatus.UserError)
        {
            _logger.LogWarning(result.UserError);
            context.Status = new Status(StatusCode.Internal, result.UserError);
        }

        return new Empty();
    }

    public override async Task<Empty> UpdateIncome(UpdateIncomeRequest request, ServerCallContext context)
    {
        var result = await _incomeService.UpdateIncomeAsync(
            request.ProfileId,
            request.IncomeId,
            request.ToBALayerAddUpdateIncomeDTO());

        if (result.ProcessStatus == BALayerCore.ProcessStatus.UserError)
        {
            _logger.LogWarning(result.UserError);
            context.Status = new Status(StatusCode.Internal, result.UserError);
        }

        return new Empty();
    }

    public override async Task<Empty> DeleteIncome(DeleteIncomeRequest request, ServerCallContext context)
    {
        var income = await _incomeService.GetIncomeByIdAsync(incomeId: request.IncomeId);
        if (income == null)
        {
            context.Status = new Status(StatusCode.NotFound, $"Income by {request.IncomeId} not found in the database!");
        }

        var result = await _incomeService.DeleteIncomeAsync(request.IncomeId);
        return new Empty();
    }

    public override async Task<IncomeDTO> GetIncomeById(GetIncomeByIdRequest request, ServerCallContext context)
    {
        var result = await _incomeService.GetIncomeByIdAsync(request.IncomeId);
        return result.Value.ToGrpcIncomeDTO();
    }

    public override async Task<GetIncomesAggregatedByPageResponse> GetIncomesAggregatedByPage(
        GetIncomesAggregatedByPageRequest request, ServerCallContext context)
    {
        if (request.PagingParams == null)
        {
            context.Status = new Status(StatusCode.InvalidArgument, "Paging parameters are required!");
            return new GetIncomesAggregatedByPageResponse();
        }

        var result = await _incomeService
            .GetIncomesAggregatedByPageAsync(
                request.ProfileId,
                request.PagingParams.ToBALayerPagingParams(),
                request.FilteringParams.ToBALayerFilteringParams());

        if (result.ProcessStatus == BALayerCore.ProcessStatus.UserError)
        {
            _logger.LogWarning(result.UserError);
            context.Status = new Status(StatusCode.Internal, result.UserError);
            return new GetIncomesAggregatedByPageResponse();
        }

        if (result.ProcessStatus == BALayerCore.ProcessStatus.SystemError)
        {
            _logger.LogWarning(result.SystemError);
            context.Status = new Status(StatusCode.Internal, result.SystemError);
            return new GetIncomesAggregatedByPageResponse();
        }

        context.ResponseTrailers.Add("X-Pagination",
            JsonSerializer.Serialize(result.Value.Metadata));


        var pagedListResult = new PagedList();
        pagedListResult.Metadata = result.Value.Metadata.ToMetadataGrpc();
        var items = result.Value
            .Select(x => Any.Pack((Google.Protobuf.IMessage)x));
        
        pagedListResult.Items.AddRange(items);

        return new GetIncomesAggregatedByPageResponse
        { 
            PagedList = pagedListResult
        };
    }
}
