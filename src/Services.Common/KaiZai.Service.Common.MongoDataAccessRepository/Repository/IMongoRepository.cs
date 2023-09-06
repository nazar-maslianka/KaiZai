namespace KaiZai.Service.Common.MongoDataAccessRepository.Repository;

public interface IMongoRepository<TDocument> where TDocument: IEntity
{
    Task CreateAsync(TDocument entity);
    void Create(TDocument entity);
    Task<ReplaceOneResult?> UpdateAsync(TDocument entity);
    ReplaceOneResult? Update(TDocument entity);
    Task<DeleteResult?> RemoveAsync(Guid id);
    DeleteResult? Remove(Guid id);
    Task<IReadOnlyCollection<TDocument>> GetAllAsync();
    IReadOnlyCollection<TDocument> GetAll();
    Task<IReadOnlyCollection<TDocument>> GetAllAsync(Expression<Func<TDocument, bool>> filter);
    IReadOnlyCollection<TDocument> GetAll(Expression<Func<TDocument, bool>> filter);
    Task<RepositoryAggregationResult<TDocument>> GetAggregateByPageAsync(
        Expression<Func<TDocument, bool>>? filterDefinition,
        FieldDefinition<TDocument>? sortDefinitionField,
        int pageNumber, int pageSize);
    Task<TDocument> GetOneAsync(Guid id);
    TDocument GetOne(Guid id);
    Task<TDocument?> GetOneAsync(Expression<Func<TDocument, bool>> filter);
    TDocument? GetOne(Expression<Func<TDocument, bool>> filter);
}