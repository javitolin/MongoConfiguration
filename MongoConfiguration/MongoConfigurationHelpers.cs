using MongoDB.Bson.Serialization;

namespace MongoConfiguration
{
    public static class MongoConfigurationHelpers
    {
        public static T DeserializeObject<T>(string serializedObject)
        {
            return BsonSerializer.Deserialize<T>(serializedObject);
        }
    }
}
