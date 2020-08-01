using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoConfiguration
{
    public static class MongoConfigurationExtensions
    {
        public static IConfigurationBuilder AddMongoProvider(this IConfigurationBuilder configuration, 
            MongoSettings mongoSettings) 
        {
            IMongoClient mongoClient = new MongoClient(mongoSettings.ConnectionString);
            IMongoDatabase database = mongoClient.GetDatabase(mongoSettings.DatabaseName);
            var mongoCollection = database.GetCollection<BsonDocument>(mongoSettings.CollectionName);

            configuration.Add(new MongoConfigurationSource(mongoCollection, mongoSettings));
            return configuration;
        }

    }
}
