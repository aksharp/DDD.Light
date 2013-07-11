using System;
using DDD.Light.CQRS.Contracts;
using DDD.Light.EventStore.Contracts;

namespace DDD.Light.AggregateCache.Contracts
{
    public interface IAggregateCache
    {
        void Configure(IEventStore eventStore, Func<Type, object> getAggregateCacheRepositoryInstance);
        TAggregate GetById<TId, TAggregate>(TId id) where TAggregate : IAggregateRoot<TId>;
        void Handle<TAggregate, TId, TEvent>(TId aggregateId, TEvent @event) where TAggregate : IAggregateRoot<TId>;
        void Clear<TId>(TId aggregateId, Type aggregateType);
    }
}
