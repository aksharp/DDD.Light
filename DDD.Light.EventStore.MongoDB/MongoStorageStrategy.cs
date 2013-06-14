using DDD.Light.EventStore.Contracts;

namespace DDD.Light.EventStore.MongoDB
{
    public class MongoStorageStrategy : IStorageStrategy
    {
        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }
        public string CollectionName { get; private set; }

        public MongoStorageStrategy(string connectionString, string databaseName, string collectionName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            CollectionName = collectionName;
        }
    }
}