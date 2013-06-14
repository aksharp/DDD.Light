using System;
using DDD.Light.CQRS.Contracts;
using DDD.Light.EventStore.Contracts;

namespace DDD.Light.AggregateStore.Contracts
{
    public interface IAggregateStore
    {
        void Configure(IEventStore eventStore);
        T GetById<T>(Guid id) where T : IAggregateRoot;
        void Handle<TAggregate, TEvent>(Guid aggregateId, TEvent @event) where TAggregate : IAggregateRoot;
    }
}
