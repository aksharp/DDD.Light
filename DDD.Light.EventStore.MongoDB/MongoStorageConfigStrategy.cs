using DDD.Light.EventStore.Contracts;

namespace DDD.Light.EventStore.MongoDB
{
    public class MongoStorageConfigStrategy : IStorageConfigStrategy
    {
        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }
        public string CollectionName { get; private set; }

        public MongoStorageConfigStrategy(string connectionString, string databaseName, string collectionName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            CollectionName = collectionName;
        }
    }
}