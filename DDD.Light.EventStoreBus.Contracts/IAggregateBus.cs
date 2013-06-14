using System;
using DDD.Light.AggregateStore.Contracts;
using DDD.Light.CQRS.Contracts;

namespace DDD.Light.AggregateBus.Contracts
{
    public interface IAggregateBus
    {
        void Subscribe(IAggregateStore aggregateStore);
        void Publish<TAggregate, TEvent>(Guid aggregateId, TEvent @event) where TAggregate : IAggregateRoot;
        void Configure(IEventBus eventBus);
    }
}