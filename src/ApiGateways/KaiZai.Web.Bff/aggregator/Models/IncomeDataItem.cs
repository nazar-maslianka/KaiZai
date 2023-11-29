using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaiZai.Web.HttpAggregator.Models;

public sealed record IncomeDataItem(
    Guid Id,
    Guid ProfileId,
    Guid CategoryId,
    DateTimeOffset IncomeDate,
    string Description,
    decimal Amount
);
