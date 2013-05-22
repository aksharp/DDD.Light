using System;

namespace DDD.Light.EventStore.Contracts
{
    public interface IEventStore
    {
        T GetById<T>(Guid id);
        object GetById(Guid id);
        void Save(AggregateEvent aggregateEvent);
        void Configure(string connectionString, string databaseName, string collectionName);
    }
}