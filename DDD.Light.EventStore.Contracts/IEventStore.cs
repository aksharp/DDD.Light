using System;
using System.Collections.Generic;

namespace DDD.Light.EventStore.Contracts
{
    public interface IEventStore
    {
        T GetById<T>(Guid id);
        T GetById<T>(Guid id, DateTime until);
        object GetById(Guid id);
        void Save(AggregateEvent aggregateEvent);
        void Configure(IStorageConfigStrategy storageConfigStrategy, IEventSerializerStrategy serializerStrategy);
        IEnumerable<AggregateEvent> GetAll();
        IEnumerable<AggregateEvent> GetAll(DateTime until);
    }
}