namespace KaiZai.Service.Common.MongoDataAccessRepository.Repository;

public interface IMongoRepository<TDocument> where TDocument: IEntity
{
    #region Get operations
    /// <summary>
    /// Asynchronously retrieves all documents from the collection.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result is an <see cref="IReadOnlyCollection{TDocument}"/> containing all documents in the collection.
    /// </returns>
    Task<IReadOnlyCollection<TDocument>> GetAllAsync();

    /// <summary>
    /// Synchronously retrieves all documents from the collection.
    /// </summary>
    /// <returns>An <see cref="IReadOnlyCollection{TDocument}"/> containing all documents in the collection.</returns>
    IReadOnlyCollection<TDocument> GetAll();

    /// <summary>
    /// Asynchronously retrieves all documents from the collection that match a specified filter expression.
    /// </summary>
    /// <param name="filter">The filter expression to apply when retrieving documents.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result is an <see cref="IReadOnlyCollection{TDocument}"/> containing matching documents.
    /// </returns>
    Task<IReadOnlyCollection<TDocument>> GetAllAsync(Expression<Func<TDocument, bool>> filter);

    /// <summary>
    /// Gets an aggregate query builder for performing aggregation operations on the collection.
    /// </summary>
    /// <param name="filterDefinition">
    /// An optional filter definition to filter the documents before aggregation.
    /// If not provided, the aggregation will operate on all documents in the collection.
    /// </param>
    /// <returns>
    /// An <see cref="IAggregateFluent{TDocument}"/> representing the aggregate query builder.
    /// </returns>
    IAggregateFluent<TDocument>? GetBaseAggregateQuery(Expression<Func<TDocument, bool>>? filterDefinition);

    /// <summary>
    /// Asynchronously performs aggregation on a collection by page, with the specified page size and number.
    /// </summary>
    /// <param name="pageSize">
    /// The maximum number of documents to include in each page of the aggregation results.
    /// </param>
    /// <param name="pageNumber">
    /// The page number, indicating which page of the aggregation results to retrieve.
    /// </param>
    /// <param name="aggregation">
    /// The aggregation query to execute on the collection.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result is a <see cref="RepositoryAggregationResult{TDocument}"/> containing the aggregation results for the specified page.
    /// </returns>
    Task<RepositoryAggregationResult<TDocument>> GetAggregateByPageAsync(
        int pageSize,
        int pageNumber,
        IAggregateFluent<TDocument> aggregation);
    
    /// <summary>
    /// Asynchronously retrieves a single document from the collection by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the document to retrieve.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The task result is the retrieved document or null if not found.
    /// </returns>
    Task<TDocument> GetOneAsync(Guid id);

    /// <summary>
    /// Synchronously retrieves a single document from the collection by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the document to retrieve.</param>
    /// <returns>
    /// The retrieved document or null if not found.
    /// </returns>
    TDocument GetOne(Guid id);

    /// <summary>
    /// Asynchronously retrieves a single document from the collection that matches a specified filter expression.
    /// </summary>
    /// <param name="filter">The filter expression to apply when retrieving the document.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The task result is the retrieved document or null if not found.
    /// </returns>
    Task<TDocument?> GetOneAsync(Expression<Func<TDocument, bool>> filter);
    
    /// <summary>
    /// Synchronously retrieves a single document from the collection that matches a specified filter expression.
    /// </summary>
    /// <param name="filter">The filter expression to apply when retrieving the document.</param>
    /// <returns>
    /// The retrieved document or null if not found.
    /// </returns>
    TDocument? GetOne(Expression<Func<TDocument, bool>> filter);
    #endregion

    #region CRUD operations
    /// <summary>
    /// Asynchronously creates a new document in the collection.
    /// </summary>
    /// <param name="entity">The document entity to be created.</param>
    Task CreateAsync(TDocument entity);

    /// <summary>
    /// Synchronously creates a new document in the collection.
    /// </summary>
    /// <param name="entity">The document entity to be created.</param>
    void Create(TDocument entity);

    /// <summary>
    /// Asynchronously updates an existing document in the collection.
    /// </summary>
    /// <param name="entity">The document entity with updated data.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result is a <see cref="ReplaceOneResult"/> representing the update result.
    /// </returns>
    Task<ReplaceOneResult?> UpdateAsync(TDocument entity);

    /// <summary>
    /// Synchronously updates an existing document in the collection.
    /// </summary>
    /// <param name="entity">The document entity with updated data.</param>
    /// <returns>A <see cref="ReplaceOneResult"/> representing the update result.</returns>
    ReplaceOneResult? Update(TDocument entity);

    /// <summary>
    /// Asynchronously removes a document from the collection by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the document to be removed.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result is a <see cref="DeleteResult"/> representing the removal result.
    /// </returns>
    Task<DeleteResult?> RemoveAsync(Guid id);

    /// <summary>
    /// Synchronously removes a document from the collection by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the document to be removed.</param>
    /// <returns>A <see cref="DeleteResult"/> representing the removal result.</returns>
    DeleteResult? Remove(Guid id);
    #endregion
}