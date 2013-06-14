using System;
using System.Reflection;
using DDD.Light.CQRS.Contracts;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.CQRS.InProcess
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        protected AggregateRoot()
        {
            
        }

        protected AggregateRoot(Guid id)
        {
            Id = id;
        }

//        public void PublishEvent<TEvent>(TEvent @event)
//        {
//            EventBus.Instance.Publish(GetType(), Id, @event);
//        }
        
        public void Publish<TAggregate, TEvent>(TEvent @event) where TAggregate : IAggregateRoot
        {
            AggregateBus.InProcess.AggregateBus.Instance.Publish<TAggregate, TEvent>(Id, @event);
            var eventType = typeof(TEvent);
            var method = GetType().GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { eventType }, null);
            method.Invoke(this, new[] { @event as Object });
        }

        public void PublishEvent<T>(T @event)
        {
            throw new NotImplementedException();
        }

//        public void PublishAndApplyEvent<TEvent>(TEvent @event)
//        {
//            EventBus.Instance.Publish(GetType(), Id, @event);
//            var eventType = typeof (TEvent);
//            var method = GetType().GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { eventType }, null);
//            method.Invoke(this, new[] { @event as Object });
//        }
    }
}