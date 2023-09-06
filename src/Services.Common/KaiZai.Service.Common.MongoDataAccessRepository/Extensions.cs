using KaiZai.Service.Common.MongoDataAccessRepository.Settings;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace KaiZai.Service.Common.MongoDataAccessRepository;

public static class Extensions
{
    public static IServiceCollection AddMongoDatabase(this IServiceCollection services, MongoConnectionSettings mongoConnectionSettings, DatabaseSettings databaseSettings)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
        services.AddSingleton(serviceProvider => 
        {
            var mongoClient = new MongoClient(mongoConnectionSettings.ConnectionString);
            return mongoClient.GetDatabase(databaseSettings.DatabaseName);
        });
        return services;
    }
}