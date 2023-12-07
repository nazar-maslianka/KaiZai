using KaiZai.Web.HttpAggregator.Core;

namespace KaiZai.Web.HttpAggregator.Models;

public sealed record GetPagedIncomesRequest(
    Guid ProfileId,
    PagingParams PagingParams,
    FilteringParams? FilteringParams = null
);