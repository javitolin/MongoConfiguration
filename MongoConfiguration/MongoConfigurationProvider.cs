using System;
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
            {
                if (MongoConfigurationSource.Optional)
                    return;

                throw new ArgumentException(
                    $"[{MongoConfigurationSource.MongoConfigurationSettings.KeyName}] not found " +
                    $"in database [{MongoConfigurationSource.MongoConfigurationSettings.ConnectionString}] under [{MongoConfigurationSource.MongoConfigurationSettings.DatabaseName}.{MongoConfigurationSource.MongoConfigurationSettings.CollectionName}] " +
                    $"under key [{MongoConfigurationSource.MongoConfigurationSettings.KeyName}]");
            }

            Data = MongoDocumentParser.Parse(item);
        }
    }
}
