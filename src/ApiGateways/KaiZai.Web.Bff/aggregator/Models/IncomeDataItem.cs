namespace KaiZai.Web.HttpAggregator.Models;

public sealed record IncomeDataItem(
    Guid Id,
    Guid ProfileId,
    Guid CategoryId,
    DateTimeOffset IncomeDate,
    string Description,
    decimal Amount
);
