using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoConfiguration
{
    public class MongoConfigurationProvider : ConfigurationProvider
    {
        private readonly IMongoCollection<BsonDocument> _mongoCollection;
        private readonly MongoSettings _mongoSettings;

        public MongoConfigurationProvider(IMongoCollection<BsonDocument> mongoCollection,
            MongoSettings mongoSettings)
        {
            _mongoCollection = mongoCollection;
            _mongoSettings = mongoSettings;
        }

        public override void Load()
        {
            var item = _mongoCollection.Find(Builders<BsonDocument>.Filter.Eq(_mongoSettings.KeyName, _mongoSettings.KeyValue)).FirstOrDefault();
            if (item == null)
                return;

            var itemDictionary = item.ToDictionary();
            foreach (var (key, value) in itemDictionary)
            {
                if (Data.ContainsKey(key))
                {
                    Data[key] = value.ToJson();
                    continue;
                }

                Data.Add(key, value.ToJson());
            }
        }
    }
}
