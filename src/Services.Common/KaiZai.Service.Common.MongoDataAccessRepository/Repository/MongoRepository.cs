namespace KaiZai.Service.Common.MongoDataAccessRepository.Repository;

/// <summary>
/// Represents a base class for MongoDB repositories that provides common functionality for data access.
/// </summary>
/// <typeparam name="TDocument">The type of documents stored in the MongoDB collection.</typeparam>
public abstract class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IEntity
{
 /// <summary>
    /// The MongoDB collection used for data access.
    /// </summary>
    protected readonly IMongoCollection<TDocument> _dbCollection;
    
    /// <summary>
    /// The filter builder for creating MongoDB filter definitions.
    /// </summary>
    protected readonly FilterDefinitionBuilder<TDocument> _filterBuilder = Builders<TDocument>.Filter;
    
    /// <summary>
    /// The sort builder for creating MongoDB sort definitions.
    /// </summary>
    protected readonly SortDefinitionBuilder<TDocument> _sortBuilder = Builders<TDocument>.Sort;

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoRepository{TDocument}"/> class.
    /// </summary>
    /// <param name="mongoDatabase">The MongoDB database to use for data access.</param>
    /// <param name="collectionName">The name of the collection within the database.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="mongoDatabase"/> or <paramref name="collectionName"/> is null or empty.
    /// </exception>
    public MongoRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        if (mongoDatabase == null)
            throw new ArgumentNullException($"{nameof(mongoDatabase)} parameter in {nameof(MongoRepository<TDocument>)} is null!");

        if (string.IsNullOrEmpty(collectionName))
            throw new ArgumentNullException($"{nameof(collectionName)} parameter in {nameof(MongoRepository<TDocument>)} is null or empty!");

        _dbCollection = mongoDatabase.GetCollection<TDocument>(collectionName);
    }
   
    #region Get operations
    /// <summary>
    /// Asynchronously retrieves all documents from the collection.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result is an <see cref="IReadOnlyCollection{TDocument}"/> containing all documents in the collection.
    /// </returns>
    public async Task<IReadOnlyCollection<TDocument>> GetAllAsync()
    {
        var documentsList = await _dbCollection
            .Find(_filterBuilder.Empty)
            .ToListAsync();
        return documentsList.AsReadOnly();
    }

    /// <summary>
    /// Synchronously retrieves all documents from the collection.
    /// </summary>
    /// <returns>An <see cref="IReadOnlyCollection{TDocument}"/> containing all documents in the collection.</returns>
    public IReadOnlyCollection<TDocument> GetAll()
    {
        return _dbCollection
            .Find(_filterBuilder.Empty)
            .ToList()
            .AsReadOnly();
    }

    /// <summary>
    /// Asynchronously retrieves all documents from the collection that match a specified filter expression.
    /// </summary>
    /// <param name="filter">The filter expression to apply when retrieving documents.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result is an <see cref="IReadOnlyCollection{TDocument}"/> containing matching documents.
    /// </returns>
    public virtual async Task<IReadOnlyCollection<TDocument>> GetAllAsync(Expression<Func<TDocument, bool>> filter)
    {
        if (filter == null)
        {
            return new List<TDocument>()
                .AsReadOnly();
        }

        var documentsList = await _dbCollection
            .Find(filter)
            .ToListAsync();
        return documentsList.AsReadOnly();
    }

    /// <summary>
    /// Synchronously retrieves all documents from the collection that match a specified filter expression.
    /// </summary>
    /// <param name="filter">The filter expression to apply when retrieving documents.</param>
    /// <returns>
    /// An <see cref="IReadOnlyCollection{TDocument}"/> containing matching documents.
    /// If the filter is null, an empty collection is returned.
    /// </returns>
    public virtual IReadOnlyCollection<TDocument> GetAll(Expression<Func<TDocument, bool>> filter)
    {
        if (filter == null)
        {
            return new List<TDocument>()
                .AsReadOnly();
        }

        return _dbCollection
            .Find(filter)
            .ToList()
            .AsReadOnly();
    }

    /// <summary>
    /// Asynchronously retrieves a single document from the collection by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the document to retrieve.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result is the retrieved document or null if not found.
    /// </returns>
    public async Task<TDocument> GetOneAsync(Guid id)
    {
        FilterDefinition<TDocument> filter = _filterBuilder.Eq(entity => entity.Id, id);
        return await _dbCollection
            .Find(filter)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Synchronously retrieves a single document from the collection by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the document to retrieve.</param>
    /// <returns>
    /// The retrieved document or null if not found.
    /// </returns>
    public TDocument GetOne(Guid id)
    {
        FilterDefinition<TDocument> filter = _filterBuilder.Eq(entity => entity.Id, id);
        return _dbCollection
            .Find(filter)
            .FirstOrDefault();
    }

    /// <summary>
    /// Asynchronously retrieves a single document from the collection that matches a specified filter expression.
    /// </summary>
    /// <param name="filter">The filter expression to apply when retrieving the document.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result is the retrieved document or null if not found.
    /// </returns>
    public virtual async Task<TDocument?> GetOneAsync(Expression<Func<TDocument, bool>> filter)
    {
        if (filter == null)
        {
            return default(TDocument);
        }
        return await _dbCollection
            .Find(filter)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Synchronously retrieves a single document from the collection that matches a specified filter expression.
    /// </summary>
    /// <param name="filter">The filter expression to apply when retrieving the document.</param>
    /// <returns>
    /// The retrieved document or null if not found.
    /// </returns>
    public virtual TDocument? GetOne(Expression<Func<TDocument, bool>> filter)
    {
        if (filter == null)
        {
            return default(TDocument);
        }
        return _dbCollection
            .Find(filter)
            .FirstOrDefault();
    }
    
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
    public virtual IAggregateFluent<TDocument>? GetBaseAggregateQuery(Expression<Func<TDocument, bool>>? filterDefinition) 
    {
        return _dbCollection.Aggregate()
            .Match(filterDefinition != null
                ? _filterBuilder.Where(filterDefinition)
                : _filterBuilder.Empty);
    }

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
    public virtual async Task<RepositoryAggregationResult<TDocument>> GetAggregateByPageAsync(
        int pageSize,
        int pageNumber, 
        IAggregateFluent<TDocument> aggregation)
    {
        var countFacet  = AggregateFacet.Create(Facets.Count,
            PipelineDefinition<TDocument, AggregateCountResult>.Create(new[]
            {
                PipelineStageDefinitionBuilder.Count<TDocument>()
            }));
        
        var dataFacet  = AggregateFacet.Create(Facets.Data,
            PipelineDefinition<TDocument, TDocument>.Create(new[]
            {
                PipelineStageDefinitionBuilder.Skip<TDocument>((pageNumber - 1) * pageSize),
                PipelineStageDefinitionBuilder.Limit<TDocument>(pageSize)
            }));
        
        var aggregationWithFacets = await aggregation
            .Facet(countFacet, dataFacet)
            .ToListAsync();
                  
        var count = (int?) aggregationWithFacets.First().Facets
            .First(x => x.Name == Facets.Count)
            .Output<AggregateCountResult>().FirstOrDefault()?
            .Count;
        
        var data = aggregationWithFacets.First()
            .Facets.First(x => x.Name == Facets.Data)
            .Output<TDocument>();
                
        return new RepositoryAggregationResult<TDocument>(data, count ?? 0);
    }
    #endregion

    #region CRUD operations
    /// <summary>
    /// Asynchronously creates a new document in the collection.
    /// </summary>
    /// <param name="entity">The document entity to be created.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public virtual async Task CreateAsync(TDocument entity)
    {
        if (entity != null)
        {
            await _dbCollection.InsertOneAsync(entity);
        }
    }

    /// <summary>
    /// Synchronously creates a new document in the collection.
    /// </summary>
    /// <param name="entity">The document entity to be created.</param>
    public virtual void Create(TDocument entity)
    {
        if (entity != null)
        {
            _dbCollection.InsertOne(entity);
        }
    }

    /// <summary>
    /// Asynchronously updates an existing document in the collection.
    /// </summary>
    /// <param name="entity">The document entity with updated data.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The task result is a <see cref="ReplaceOneResult"/> representing the update result.
    /// </returns>
    public virtual async Task<ReplaceOneResult?> UpdateAsync(TDocument entity)
    {
        if (entity != null)
        {
            FilterDefinition<TDocument> filter = _filterBuilder.Eq(x => x.Id, entity.Id);
            return await _dbCollection.ReplaceOneAsync(filter, entity); 
        }
        return null;
    }

    /// <summary>
    /// Synchronously updates an existing document in the collection.
    /// </summary>
    /// <param name="entity">The document entity with updated data.</param>
    /// <returns>
    /// A <see cref="ReplaceOneResult"/> representing the update result.
    /// </returns>
    public virtual ReplaceOneResult? Update(TDocument entity)
    {
        if (entity != null)
        {
            FilterDefinition<TDocument> filter = _filterBuilder.Eq(x => x.Id, entity.Id);
            return _dbCollection.ReplaceOne(filter, entity); 
        }
        return null;
    }

    /// <summary>
    /// Asynchronously removes a document from the collection by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the document to be removed.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The task result is a <see cref="DeleteResult"/> representing the removal result.
    /// </returns>
    public virtual async Task<DeleteResult?> RemoveAsync(Guid id)
    {
        FilterDefinition<TDocument> filter = _filterBuilder.Eq(entity => entity.Id, id);
        return await _dbCollection.DeleteOneAsync(filter);   
    }

    /// <summary>
    /// Synchronously removes a document from the collection by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the document to be removed.</param>
    /// <returns>
    /// A <see cref="DeleteResult"/> representing the removal result.
    /// </returns>
    public virtual DeleteResult? Remove(Guid id)
    {
        FilterDefinition<TDocument> filter = _filterBuilder.Eq(entity => entity.Id, id);
        return _dbCollection.DeleteOne(filter);  
    }
    #endregion
}