using System;
using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.EventStore.Contracts
{
    public interface IEventStore
    {
        T GetById<T>(Guid id);
        T GetById<T>(Guid id, DateTime until);
        object GetById(Guid id);
        void Save(AggregateEvent aggregateEvent);
        void Configure(IStorageConfigStrategy storageConfigStrategy, IEventSerializerStrategy serializerStrategy, IEventStoreBus bus);
        IEnumerable<AggregateEvent> GetAll();
        IEnumerable<AggregateEvent> GetAll(DateTime until);
        void Subscribe(IEntity aggregate);
        void SubscribeSince(IEntity aggregate, DateTime since);
        void Publish<T>(Type aggregateType, Guid aggregateId, T @event);

        T GetSubscribedById<T>(Guid id) where T : IEntity;
        object GetSubscribedById(Guid id);
    }
}