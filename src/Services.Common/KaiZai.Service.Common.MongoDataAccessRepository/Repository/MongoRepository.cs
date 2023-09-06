namespace KaiZai.Service.Common.MongoDataAccessRepository.Repository;

///<summary>
///An abstract repository class for access mongo collections.
///</summary>
public abstract class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IEntity
{
    protected readonly IMongoCollection<TDocument> _dbCollection;
    protected readonly FilterDefinitionBuilder<TDocument> _filterBuilder = Builders<TDocument>.Filter;
    protected readonly SortDefinitionBuilder<TDocument> _sortBuilder = Builders<TDocument>.Sort;

    ///<summary>
    ///Initialize access to mongo collection.
    ///</summary>
    /// <param name="mongoDatabase">Instance of mongo database. If null, returns ArgumentNullException</param>
    /// <param name="collectionName">Name of mongo collection. If null or empty, returns ArgumentNullException</param>
    public MongoRepository(IMongoDatabase mongoDatabase, string collectionName)
    {
        if (mongoDatabase == null) throw new ArgumentNullException($"Parameter: {nameof(mongoDatabase)} in {nameof(MongoRepository<TDocument>)} is null!");
        if (string.IsNullOrEmpty(collectionName)) throw new ArgumentNullException($"Parameter: {nameof(collectionName)} in {nameof(MongoRepository<TDocument>)} is null or empty!");

        _dbCollection = mongoDatabase.GetCollection<TDocument>(collectionName);
    }
   
    #region Get operations
    public async Task<IReadOnlyCollection<TDocument>> GetAllAsync()
    {
        var documentsList = await _dbCollection
            .Find(_filterBuilder.Empty)
            .ToListAsync();
        return documentsList.AsReadOnly();
    }
    public IReadOnlyCollection<TDocument> GetAll()
    {
        return _dbCollection
            .Find(_filterBuilder.Empty)
            .ToList()
            .AsReadOnly();
    }
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
    public async Task<TDocument> GetOneAsync(Guid id)
    {
        FilterDefinition<TDocument> filter =_filterBuilder.Eq(entity => entity.Id, id);
        return await _dbCollection
            .Find(filter)
            .FirstOrDefaultAsync();
    }
    public TDocument GetOne(Guid id)
    {
        FilterDefinition<TDocument> filter =_filterBuilder.Eq(entity => entity.Id, id);
        return _dbCollection
            .Find(filter)
            .FirstOrDefault();
    }
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
    ///Returns filtered, sorted and paged documents and total count of elements in database collection
    ///</summary>
    /// <param name="filterDefinition"> Defines logic for filtering. Could be null</param>
    /// <param name="sortDefinitionField"> Defines field of class for sorting. Could be null </param>
    /// <param name="pagingParams"> Settings for skip and limit pages<
    public virtual async Task<RepositoryAggregationResult<TDocument>> GetAggregateByPageAsync(
        Expression<Func<TDocument, bool>>? filterDefinition, 
        FieldDefinition<TDocument>? sortDefinitionField, 
        int pageNumber, int pageSize)
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
                PipelineStageDefinitionBuilder.Limit<TDocument>(pageSize),
            }));

        var aggregation = await  _dbCollection.Aggregate()
              .Match(filterDefinition != null 
                  ? _filterBuilder.Where(filterDefinition)
                  : _filterBuilder.Empty)
              .Facet(countFacet, dataFacet)
              .ToListAsync();
            
        var count = (int?) aggregation.First().Facets
            .First(x => x.Name == Facets.Count)
            .Output<AggregateCountResult>().FirstOrDefault()?
            .Count;
        
        var data = aggregation.First()
            .Facets.First(x => x.Name == Facets.Data)
            .Output<TDocument>();
                
        return new RepositoryAggregationResult<TDocument>(data, count ?? 0);
    }
    #endregion

    #region CRUD operations
    public virtual async Task CreateAsync(TDocument entity)
    {
        if (entity != null)
        {
            await _dbCollection.InsertOneAsync(entity);
        }
    }
    public virtual void Create(TDocument entity)
    {
        if (entity != null)
        {
            _dbCollection.InsertOne(entity);
        }
    }
    public virtual async Task<ReplaceOneResult?> UpdateAsync(TDocument entity)
    {
        if (entity != null)
        {
            FilterDefinition<TDocument> filter =_filterBuilder.Eq(x => x.Id, entity.Id);
            return await _dbCollection.ReplaceOneAsync(filter, entity); 
        }
        return null;
    }
    public virtual ReplaceOneResult? Update(TDocument entity)
    {
        if (entity != null)
        {
            FilterDefinition<TDocument> filter =_filterBuilder.Eq(x => x.Id, entity.Id);
            return _dbCollection.ReplaceOne(filter, entity); 
        }
        return null;
    }
    public virtual async Task<DeleteResult?> RemoveAsync(Guid id)
    {
        FilterDefinition<TDocument> filter =_filterBuilder.Eq(entity => entity.Id, id);
        return await _dbCollection.DeleteOneAsync(filter);   
    }
    public virtual DeleteResult? Remove(Guid id)
    {
        FilterDefinition<TDocument> filter =_filterBuilder.Eq(entity => entity.Id, id);
        return _dbCollection.DeleteOne(filter);  
    }
    #endregion
}