using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoConfiguration
{
    public class MongoConfigurationProvider : ConfigurationProvider
    {
        private readonly IMongoCollection<BsonDocument> _mongoCollection;
        private readonly string _keyName;
        private string _keyValue;

        public MongoConfigurationProvider(IMongoCollection<BsonDocument> mongoCollection, string keyName, string keyValue)
        {
            _mongoCollection = mongoCollection;
            _keyName = keyName;
            _keyValue = keyValue;
        }

        public override void Load()
        {
            var item = _mongoCollection.Find(Builders<BsonDocument>.Filter.Eq(_keyName, _keyValue)).FirstOrDefault();
            if (item == null)
                return;

            if (Data.ContainsKey(_keyValue))
            {
                Data[_keyValue] = item.ToJson();
                return;
            }

            Data.Add(_keyValue, item.ToJson());
        }
    }
}
