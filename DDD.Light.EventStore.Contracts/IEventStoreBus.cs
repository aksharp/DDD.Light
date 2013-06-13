using System;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.EventStore.Contracts
{
    public interface IEventStoreBus
    {
        void Subscribe(IEntity aggregate);
        void Publish<T>(Type aggregateType, Guid aggregateId, T @event);
        IEntity GetSubscribedAggregateById(Guid id);
    }
}