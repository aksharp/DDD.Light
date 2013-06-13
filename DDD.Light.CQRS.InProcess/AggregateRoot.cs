using System;
using System.Reflection;
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

        protected virtual void PublishEvent<TEvent>(TEvent @event)
        {
            EventBus.Instance.Publish(GetType(), Id, @event);
        }
        
        protected virtual void PublishAndApplyEvent<TEvent>(TEvent @event)
        {
            EventBus.Instance.Publish(GetType(), Id, @event);
            var eventType = typeof (TEvent);
            var method = GetType().GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { eventType }, null);
            method.Invoke(this, new[] { @event as Object });
        }
    }
}