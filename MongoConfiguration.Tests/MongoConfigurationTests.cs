using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace MongoConfiguration.Tests
{
    public class MongoConfigurationTests
    {
        [Fact]
        public void GetConfiguration_MyNumbers_ReturnsListOfNumbers()
        {
            MongoSettings mongoSettings = new MongoSettings
            {
                ConnectionString = "mongodb+srv://test_user:test_user@cluster0.sn9q9.azure.mongodb.net/test",
                DatabaseName = "configuration_db",
                CollectionName = "configuration_collection",
                KeyName = "MyKey",
                KeyValue = "mykeyvalue"
            };

            var builder = new ConfigurationBuilder()
                .AddMongoProvider(mongoSettings);

            var numbers = builder.Build()["MyNumbers"].DeserializeMongoObject<List<int>>();
            Assert.Equal(5, numbers.Count);
        }

        [Fact]
        public void GetConfiguration_SettingsFromJson_ReturnsListOfNumbers()
        {

            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json");
            var conf = builder.Build();

            MongoSettings settings = new MongoSettings();
            conf.GetSection("MongoSettings").Bind(settings);

            builder = new ConfigurationBuilder()
                .AddConfiguration(conf)
                .AddMongoProvider(settings);

            conf = builder.Build();

            var numbers = conf["MyNumbers"].DeserializeMongoObject<List<int>>();
            Assert.Equal(5, numbers.Count);
        }

        [Fact]
        public void GetConfiguration_SettingsFromJsonOverride_ReturnsListOfNumbersFromJson()
        {

            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json");
            var conf = builder.Build();

            MongoSettings settings = new MongoSettings();
            conf.GetSection("MongoSettings").Bind(settings);

            builder = new ConfigurationBuilder()
                //.AddMongoProvider(settings)
                .AddConfiguration(conf);

            conf = builder.Build();

            var numbers = conf["MyNumbers"].DeserializeMongoObject<List<int>>();
            Assert.Equal(3, numbers.Count);
        }
    }
}