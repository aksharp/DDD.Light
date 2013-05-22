using System;
using DDD.Light.EventStore.Contracts;

namespace DDD.Light.Messaging.Contracts
{
    public interface IEventBus
    {
        void Subscribe<T>(IEventHandler<T> handler);
        void Subscribe<T>(Action<T> handler);
        void Publish<T>(Guid aggregateId, T @event);
        void Configure(IEventStore instance);
    }
}