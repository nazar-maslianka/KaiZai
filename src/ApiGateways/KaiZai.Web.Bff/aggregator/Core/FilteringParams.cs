namespace KaiZai.Web.HttpAggregator.Core;
/// <summary>
/// Represents an object for filtering.
/// </summary>
public sealed record FilteringParams
{
    public DateTimeOffset StartDate { get; init; } = DateTimeOffset.UtcNow.AddDays(-30);
    public DateTimeOffset EndDate { get; init; } = DateTimeOffset.UtcNow;
}