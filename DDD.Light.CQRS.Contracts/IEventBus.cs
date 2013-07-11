using System;
using DDD.Light.EventStore.Contracts;

namespace DDD.Light.CQRS.Contracts
{
    public interface IEventBus
    {
        void Subscribe<T>(IEventHandler<T> handler);
        void Subscribe<T>(Action<T> handler);
        void Publish<TId, T>(Type aggregateType, TId aggregateId, T @event);
        void Publish<TAggregate, TId, T>(TId aggregateId, T @event);
        void Configure(IEventStore eventStore, IEventSerializationStrategy eventSerializationStrategy, bool checkLatestEventTimestampPriorToSavingToEventStore);
        void RestoreReadModel();
        void RestoreReadModel(DateTime until);
        IEventStore GetEventStore();
    }
}