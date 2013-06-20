using System;
using DDD.Light.AggregateCache.Contracts;
using DDD.Light.CQRS.Contracts;

namespace DDD.Light.AggregateBus.Contracts
{
    public interface IAggregateBus
    {
        void Subscribe(IAggregateCache aggregateCache);
        void Publish<TAggregate, TEvent>(Guid aggregateId, TEvent @event) where TAggregate : IAggregateRoot;
        void Configure(IEventBus eventBus);
    }
}