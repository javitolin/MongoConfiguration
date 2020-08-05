using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoConfiguration
{
    public static class MongoConfigurationExtensions
    {
        public static IConfigurationBuilder AddMongoProvider(this IConfigurationBuilder configuration,
            MongoConfigurationSettings mongoConfigurationSettings)
        {
            IMongoClient mongoClient = new MongoClient(mongoConfigurationSettings.ConnectionString);
            IMongoDatabase database = mongoClient.GetDatabase(mongoConfigurationSettings.DatabaseName);
            var mongoCollection = database.GetCollection<BsonDocument>(mongoConfigurationSettings.CollectionName);

            configuration.Add(new MongoConfigurationSource(mongoCollection, mongoConfigurationSettings));
            return configuration;
        }

        public static IConfigurationBuilder AddMongoConfiguration(this IConfigurationBuilder configuration,
            Action<MongoConfigurationSettings> options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));
            var configurationOptions = new MongoConfigurationSettings();
            options(configurationOptions);
            configuration.Add(new MongoConfigurationSource(configurationOptions));
            return configuration;
        }
    }
}
