using KaiZai.Service.Common.MongoDataAccessRepository.Settings;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace KaiZai.Service.Common.MongoDataAccessRepository;

public static class Extensions
{
    /// <summary>
    /// Injects Mongo database to services of project.
    ///</summary>
    /// <param name="serviceSettings">Settings with service name for Mongo collection</param>
    /// <param name="mongoConnectionSettings">Settings for connecting to Mongo database</param>
    public static IServiceCollection AddMongoDatabase(this IServiceCollection services, ServiceSettings serviceSettings, MongoConnectionSettings mongoConnectionSettings)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
        services.AddSingleton(() => 
        {
            var mongoClient = new MongoClient(mongoConnectionSettings.ConnectionString);
            return mongoClient.GetDatabase(serviceSettings.ServiceName);
        });
        return services;
    }
}