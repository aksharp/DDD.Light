using System;
using DDD.Light.EventStore.Contracts;

namespace DDD.Light.EventBus.Contracts
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(IEventHandler<TEvent> handler);
        void Subscribe<TEvent>(Action<TEvent> handler);
        void Publish<TId, TEvent>(Type aggregateType, TId id, TEvent @event);
        void Configure(IEventStore eventStore, IEventSerializationStrategy eventSerializationStrategy, bool checkLatestEventTimestampPriorToSavingToEventStore);
    }
}