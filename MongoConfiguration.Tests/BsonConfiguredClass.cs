using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoConfiguration.Tests
{
    [BsonIgnoreExtraElements]
    public class BsonConfiguredClass
    {

        [BsonElement("first_name")]
        public string StudentName { get; set; }

        [BsonElement("age")]
        public int StudentAge { get; set; }

        [BsonElement("grades")]
        public IEnumerable<int> StudentGrades { get; set; }

        [BsonElement("lessons")]
        public Dictionary<string, string> LessonsDaysDictionary { get; set; }
    }
}
