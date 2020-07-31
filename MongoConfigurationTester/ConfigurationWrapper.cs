using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoConfiguration
{
    [BsonIgnoreExtraElements]
    public class MyConfiguration
    {
        public string MyKey;
        public List<int> MyNumbers;
    }
}
