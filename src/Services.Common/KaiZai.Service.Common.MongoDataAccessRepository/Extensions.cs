using KaiZai.Service.Common.MongoDataAccessRepository.Settings;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace KaiZai.Service.Common.MongoDataAccessRepository;

public static class Extensions
{
    /// <summary>
    /// Adds Mongo database to to the collection.
    ///</summary>
    /// <remarks> 
    /// Important! Do not forget to configure ServiceSettings and MongoConnectionSettings sections in your project.
    /// </remarks>
    /// <param name="collection"></param>
    /// <param name="serviceSettings">Settings with service name for Mongo collection</param>
    /// <param name="mongoConnectionSettings">Settings for connecting to Mongo database</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="serviceSettings"/> or <paramref name="mongoConnectionSettings"/> is null.</exception>
    public static IServiceCollection AddMongoDatabase(this IServiceCollection collection, 
        ServiceSettings serviceSettings, 
        MongoConnectionSettings mongoConnectionSettings)
    {
         if (serviceSettings == null)
        {
            throw new ArgumentNullException($"{nameof(serviceSettings)} is missing in the configuration.");
        }
        if (mongoConnectionSettings == null)
        {
            throw new ArgumentNullException($"{nameof(mongoConnectionSettings)} is missing in the configuration.");
        }
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
        collection.AddSingleton<IMongoDatabase>(
            new MongoClient(mongoConnectionSettings.ConnectionString).GetDatabase(serviceSettings.ServiceName)
        );
        return collection;
    }
}