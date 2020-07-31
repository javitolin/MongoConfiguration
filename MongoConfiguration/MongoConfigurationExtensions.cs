using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace MongoConfiguration
{
    public static class MongoConfigurationExtensions
    {
        public static IConfigurationBuilder AddMongoProvider(this IConfigurationBuilder configuration, 
            IMongoCollection<BsonDocument> mongoCollection, string keyName, string keyValue) 
        {
            configuration.Add(new MongoConfigurationSource(mongoCollection, keyName, keyValue));
            return configuration;
        }

        public static IConfigurationBuilder AddMongoProvider(this IConfigurationBuilder configuration, 
            string connectionString, string databaseName, string collectionName, string keyName, string keyValue) 
        {
            IMongoClient mongoClient = new MongoClient(connectionString);
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            var mongoCollection = database.GetCollection<BsonDocument>(collectionName);

            configuration.Add(new MongoConfigurationSource(mongoCollection, keyName, keyValue));
            return configuration;
        }

    }
}
