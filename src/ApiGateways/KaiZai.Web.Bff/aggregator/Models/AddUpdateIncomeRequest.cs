namespace KaiZai.Web.HttpAggregator.Models;

public sealed record AddUpdateIncomeRequest (
    Guid CategoryId,
    DateTimeOffset IncomeDate,
    string Description,
    decimal Amount
);