using Microsoft.Extensions.Configuration;
using MongoConfiguration;
using System.Collections.Generic;
using Xunit;

namespace MongoConfigurationTester
{
    public class TestBase
    {
        IConfigurationBuilder _builder;

        public TestBase()
        {
            var builder = new ConfigurationBuilder()
               .AddMongoProvider("mongodb+srv://test_user:test_user@cluster0.sn9q9.azure.mongodb.net/test", "configuration_db",
                   "configuration_collection", "MyKey", "mykeyvalue");

            _builder = builder;
        }

        [Fact]
        public void GetKey_MyNumbers_ReturnsListOfNumbers()
        {
            var numbers = _builder.Build()["MyNumbers"].DeserializeMongoObject<List<int>>();
            Assert.Equal(5, numbers.Count);
        }
    }
}
