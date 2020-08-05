using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoConfiguration
{
    public class MongoConfigurationSource : IConfigurationSource
    {
        public IMongoCollection<BsonDocument> MongoCollection { get; }
        public MongoConfigurationSettings MongoConfigurationSettings { get; }

        public MongoConfigurationSource(IMongoCollection<BsonDocument> mongoCollection, 
            MongoConfigurationSettings mongoConfigurationSettings)
        {
            MongoCollection = mongoCollection;
            MongoConfigurationSettings = mongoConfigurationSettings;
        }

        public MongoConfigurationSource(MongoConfigurationSettings mongoConfigurationSettings)
        {
            MongoConfigurationSettings = mongoConfigurationSettings;
            IMongoClient mongoClient = new MongoClient(mongoConfigurationSettings.ConnectionString);
            IMongoDatabase database = mongoClient.GetDatabase(mongoConfigurationSettings.DatabaseName);
            MongoCollection = database.GetCollection<BsonDocument>(mongoConfigurationSettings.CollectionName);
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MongoConfigurationProvider(this);
        }
    }
}
