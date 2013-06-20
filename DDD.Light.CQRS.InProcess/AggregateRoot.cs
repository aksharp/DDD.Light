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
            try
            {
                var publishMethod = typeof (AggregateBus.InProcess.AggregateBus).GetMethod("Publish");
                var genericPublishMethod = publishMethod.MakeGenericMethod(new[] {GetType(), typeof (TEvent)});
                genericPublishMethod.Invoke(AggregateBus.InProcess.AggregateBus.Instance, new[] {Id, @event as Object});
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("DDD.Light.CQRS.InProcess.AggregateRoot -> PublishOnAggregateBusThroughReflection: Failed to get and invoke Publish method on AggregateBus.Instance. Event type {0} did not get published", typeof(TEvent)), ex);
            }
        }

        private void ApplyEventOnAggregate<TEvent>(TEvent @event)
        {
            try
            {
                var method = GetType().GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] {typeof (TEvent)}, null);
                method.Invoke(this, new[] {@event as Object});
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("DDD.Light.CQRS.InProcess.AggregateRoot -> ApplyEventOnAggregate: Failed to apply event on aggregate type: {0} through reflection. Event type {1} did not get applied", GetType(), typeof(TEvent)), ex);
            }
        }
    }
}