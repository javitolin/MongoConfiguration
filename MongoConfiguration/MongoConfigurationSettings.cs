namespace MongoConfiguration
{
    public class MongoConfigurationSettings
    {
        /// <summary>
        /// The connection string to use to connect to the MongoDB server
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// The database name to connect to
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// The collection inside the database to use
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        /// The name of the key for which the value will be searched for
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// The value of the key that will be used to search for the required document
        /// </summary>
        public object KeyValue { get; set; }
    }
}
