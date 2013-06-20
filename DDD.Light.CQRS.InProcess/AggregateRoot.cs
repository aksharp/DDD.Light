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

        public void PublishAndApplyEvent<TAggregate, TEvent>(TEvent @event) where TAggregate : IAggregateRoot
        {
            AggregateBus.InProcess.AggregateBus.Instance.Publish<TAggregate, TEvent>(Id, @event);
            ApplyEventOnAggregate(@event);
        }

        public void PublishAndApplyEvent<TEvent>(TEvent @event)
        {
            PublishOnAggregateBusThroughReflection(@event);
            ApplyEventOnAggregate(@event);
        }

        private void PublishOnAggregateBusThroughReflection<TEvent>(TEvent @event)
        {
            var publishMethod = typeof (AggregateBus.InProcess.AggregateBus).GetMethod("Publish");
            var genericPublishMethod = publishMethod.MakeGenericMethod(new[] {GetType(), typeof (TEvent)});
            genericPublishMethod.Invoke(AggregateBus.InProcess.AggregateBus.Instance, new[] {Id, @event as Object});
        }

        private void ApplyEventOnAggregate<TEvent>(TEvent @event)
        {
            var method = GetType().GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(TEvent) }, null);
            method.Invoke(this, new[] {@event as Object});
        }
    }
}