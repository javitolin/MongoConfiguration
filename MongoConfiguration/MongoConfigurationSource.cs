using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoConfiguration
{
    public class MongoConfigurationSource : IConfigurationSource
    {
        private readonly IMongoCollection<BsonDocument> _mongoCollection;
        private readonly string _keyName;
        private readonly string _keyValue;

        public MongoConfigurationSource(IMongoCollection<BsonDocument> mongoCollection, string keyName, string keyValue)
        {
            _mongoCollection = mongoCollection;
            _keyName = keyName;
            _keyValue = keyValue;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MongoConfigurationProvider(_mongoCollection, _keyName, _keyValue);
        }
    }
}
