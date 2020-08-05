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
            MongoConfigurationSettings mongoConfigurationSettings = new MongoConfigurationSettings
            {
                ConnectionString = "mongodb+srv://test_user:test_user@cluster0.sn9q9.azure.mongodb.net/test",
                DatabaseName = "configuration_db",
                CollectionName = "configuration_collection",
                KeyName = "MyKey",
                KeyValue = "mykeyvalue"
            };

            var builder = new ConfigurationBuilder()
                .AddMongoProvider(mongoConfigurationSettings);

            List<int> numbers = new List<int>();
            builder.Build().GetSection("MyNumbers").Bind(numbers);
            Assert.Equal(5, numbers.Count);
        }

        [Fact]
        public void GetConfiguration_SettingsFromJson_ReturnsListOfNumbers()
        {

            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json");
            var conf = builder.Build();

            MongoConfigurationSettings configurationSettings = new MongoConfigurationSettings();
            conf.GetSection("MongoConfigurationSettings").Bind(configurationSettings);

            builder = new ConfigurationBuilder()
                .AddMongoProvider(configurationSettings);

            conf = builder.Build();

            List<int> numbers = new List<int>();
            conf.GetSection("MyNumbers").Bind(numbers);
            Assert.Equal(5, numbers.Count);
            Assert.Equal(1, numbers[0]);
        }

        [Fact]
        public void GetConfiguration_SettingsFromJsonOverride_ReturnsListOfNumbersFromJson()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json");
            var conf = builder.Build();

            MongoConfigurationSettings configurationSettings = new MongoConfigurationSettings();
            conf.GetSection("MongoConfigurationSettings").Bind(configurationSettings);

            builder = new ConfigurationBuilder()
                .AddMongoProvider(configurationSettings)
                .AddJsonFile("config.json");

            conf = builder.Build();

            List<int> numbers = new List<int>();
            conf.GetSection("MyNumbers").Bind(numbers);
            Assert.Equal(5, numbers.Count);
            Assert.Equal(12232, numbers[0]);
        }

        [Fact]
        public void GetConfiguration_SettingsFromJson_ReturnsSomeObject()
        {

            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json");
            var conf = builder.Build();

            MongoConfigurationSettings configurationSettings = new MongoConfigurationSettings();
            conf.GetSection("MongoConfigurationSettings").Bind(configurationSettings);

            builder = new ConfigurationBuilder()
                .AddMongoProvider(configurationSettings);

            conf = builder.Build();
            var customObject = conf.GetSection("SomeObject");
            Assert.Equal("InternalValue", customObject["InternalKey"]);
        }

        [Fact]
        public void GetConfiguration_SettingsFromJsonOverride_ReturnsNewSomeObject()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json");
            var conf = builder.Build();

            MongoConfigurationSettings configurationSettings = new MongoConfigurationSettings();
            conf.GetSection("MongoConfigurationSettings").Bind(configurationSettings);

            builder = new ConfigurationBuilder()
                .AddMongoProvider(configurationSettings)
                .AddJsonFile("config.json");

            conf = builder.Build();
            var customObject = conf.GetSection("SomeObject");
            Assert.Equal("one", customObject["one"]);
            Assert.Equal("otherValue", customObject["other"]);
            Assert.Equal("JsonInternal", customObject["InternalKey"]);
        }
    }
}