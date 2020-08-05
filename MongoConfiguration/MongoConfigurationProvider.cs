using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoConfiguration
{
    public class MongoConfigurationProvider : ConfigurationProvider
    {
        public MongoConfigurationSource MongoConfigurationSource { get; }

        public MongoConfigurationProvider(MongoConfigurationSource mongoConfigurationSource)
        {
            MongoConfigurationSource = mongoConfigurationSource;
        }

        public override void Load()
        {
            var item = MongoConfigurationSource.MongoCollection
                .Find(Builders<BsonDocument>.Filter.Eq(MongoConfigurationSource.MongoConfigurationSettings.KeyName,
                    MongoConfigurationSource.MongoConfigurationSettings.KeyValue)).FirstOrDefault();

            if (item == null)
                return;

            Data = MongoDocumentParser.Parse(item);
        }
    }
}
