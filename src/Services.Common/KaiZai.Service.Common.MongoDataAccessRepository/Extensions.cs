using KaiZai.Service.Common.MongoDataAccessRepository.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace KaiZai.Service.Common.MongoDataAccessRepository;

public static class Extensions
{
     /// <summary>
    /// Set up settings for using Mongo database in project.
    /// Important! Do not forget to configure ServiceSettings and MongoConnectionSettings sections in launch.json of your project.
    ///</summary>
    public static IServiceCollection AddMongoDatabase(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
        services.AddSingleton(serviceProvider => 
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
            var mongoConnectionSettings = configuration.GetSection(nameof(MongoConnectionSettings)).Get<MongoConnectionSettings>();
            var mongoClient = new MongoClient(mongoConnectionSettings.ConnectionString);
            return mongoClient.GetDatabase(serviceSettings.ServiceName);
        });
        return services;
    }
}