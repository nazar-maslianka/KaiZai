namespace KaiZai.Services.Incomes.BAL.Core;

/// <summary>
/// Represents an object for filtering.
/// </summary>
public sealed record FilteringParams
{
    public DateTimeOffset? StartDate { get; init; }
    public DateTimeOffset? EndDate { get; init; }
}