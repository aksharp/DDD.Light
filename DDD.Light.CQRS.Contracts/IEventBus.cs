using System;
using DDD.Light.EventStore.Contracts;

namespace DDD.Light.CQRS.Contracts
{
    public interface IEventBus
    {
        void Subscribe<T>(IEventHandler<T> handler);
        void Subscribe<T>(Action<T> handler);
        void Publish<T>(Type aggregateType, Guid aggregateId, T @event);
        void Publish<TAggregate, T>(Guid aggregateId, T @event);
        void Configure(IEventStore instance);
        void RestoreReadModel();
        void RestoreReadModel(DateTime until);
        Guid GetUniqueInstanceID();
        IEventStore GetEventStore();
    }
}