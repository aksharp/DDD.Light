using System;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.CQRS.InProcess
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot()
        {
            
        }

        protected AggregateRoot(Guid id)
        {
            Id = id;
        }

        protected void PublishEvent<TEvent>(TEvent @event)
        {
            EventBus.Instance.Publish(GetType(), Id, @event);
        }
    }
}