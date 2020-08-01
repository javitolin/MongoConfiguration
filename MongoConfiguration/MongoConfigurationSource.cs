using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoConfiguration
{
    public class MongoConfigurationSource : IConfigurationSource
    {
        private readonly IMongoCollection<BsonDocument> _mongoCollection;
        private readonly MongoSettings _mongoSettings;

        public MongoConfigurationSource(IMongoCollection<BsonDocument> mongoCollection, 
            MongoSettings mongoSettings)
        {
            _mongoCollection = mongoCollection;
            _mongoSettings = mongoSettings;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MongoConfigurationProvider(_mongoCollection, _mongoSettings);
        }
    }
}
