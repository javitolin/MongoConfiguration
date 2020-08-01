using MongoDB.Bson.Serialization;

namespace MongoConfiguration
{
    public static class MongoConfigurationHelpers
    {
        public static T DeserializeMongoObject<T>(this string configurationValue)
        {
            return BsonSerializer.Deserialize<T>(configurationValue);
        }
    }
}
