using KaiZai.Web.HttpAggregator.Core;

namespace KaiZai.Web.HttpAggregator.Config;

public class UrlsConfig
{
    public class IncomesOperations
    {
        // grpc call under REST must go trough port 80
        public static string GetIncome(Guid profileId, Guid id) => $"api/profile/{profileId}/incomes/{id}";

        public static string GetIncomesAggregatedByPage(Guid profileId, 
            PagingParams pagingParams,
            FilteringParams filteringParams
        ) => $"/profile/{profileId}/incomes?pageNumber={pagingParams.PageNumber}&pageSize={pagingParams.PageSize}&startDate={filteringParams.StartDate}&endDate={filteringParams.EndDate}";

        public static string AddIncome() => "api/profile/{profileId}/incomes";
    }

    public string Incomes { get; set; }
    public string GrpcIncomes { get; set; }
}