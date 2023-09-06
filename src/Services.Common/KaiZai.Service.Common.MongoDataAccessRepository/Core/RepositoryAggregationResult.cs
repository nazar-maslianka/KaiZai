namespace KaiZai.Service.Common.MongoDataAccessRepository.Core;

public sealed class RepositoryAggregationResult<TDocument> where TDocument : IEntity
{
    public RepositoryAggregationResult(IReadOnlyList<TDocument> data,
        int count)
    {
        Data = data;
        Count = count;
    }
    public IReadOnlyList<TDocument> Data { get; }
    public int Count { get; }
}