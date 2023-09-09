using KaiZai.Service.Categories.API.Data.Entities;
using KaiZai.Service.Categories.API.Data.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace KaiZai.Service.Categories.UnitTests;

public sealed class DbFixture : IDisposable
{
    public readonly Category[] seedCategories;
    public readonly IMongoDatabase database;
    private readonly MongoClient _mongoClient;
    private readonly string _dbName;
    public DbFixture()
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        
        seedCategories = GetSeedCategories();
        _dbName = ConfigurationValues.TestDbName;
        _mongoClient = new MongoClient(ConfigurationValues.MongoConnectionString);
        database = _mongoClient.GetDatabase(_dbName);
    }
    #region Public methods
    public void Seed()
    {
        var categoriesCollection = database.GetCollection<Category>($"{nameof(KaiZai.Service.Categories)}");
        if (categoriesCollection.CountDocuments(FilterDefinition<Category>.Empty) == 0)
        {
            categoriesCollection.InsertMany(seedCategories);
        }
    }

    public void Dispose()
    {
        _mongoClient.DropDatabase(_dbName);
    }
    #endregion
    #region Internal methods
    private static Category[] GetSeedCategories()
    {
        return new Category[]
        {
            new Category
            {
                Id = new Guid("373b5c56-df08-4774-b096-1e2bba1c0b36"),
                UserId = new Guid("fb12fd97-e08a-4793-8a17-f9bd70233749"),
                Name = "Test_Groceries",
                CategoryType = CategoryType.Expense
            },
            new Category
            {
                Id = new Guid("3eb77f9e-85ff-4794-8a0b-7e48307326ee"),
                UserId = new Guid("fb12fd97-e08a-4793-8a17-f9bd70233749"),
                Name = "Test_Cafe",
                CategoryType = CategoryType.Expense
            },
            new Category
            {
                Id = new Guid("a44817bd-1099-4f34-a02c-1f31d9794f36"),
                UserId = new Guid("208b0ac2-a0ca-438c-a5e5-fa7ab4dfa10a"),
                Name = "Test_Salary",
                CategoryType = CategoryType.Income
            },
            new Category
            {
                Id = new Guid("1f67ab91-a949-4ae4-9b1c-35f65edfa60f"),
                UserId = new Guid("e106b3d6-a418-4569-afbb-ff680b41f7a0"),
                Name = "Test_Transportation",
                CategoryType = CategoryType.Expense
            }
        };
    }
    #endregion
}