using System;
using System.Reflection;
using DDD.Light.EventBus.Contracts;

namespace DDD.Light.EventBus.InProcess
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
    {
        protected AggregateRoot(TId id) : base(id)
        {
        }
        
        public void PublishEvent<TEvent>(TEvent @event)
        {
            EventBus.Instance.Publish(GetType(), Id, @event);
        }
        
        public void PublishAndApplyEvent<TEvent>(TEvent @event)
        {
            EventBus.Instance.Publish(GetType(), Id, @event);
            ApplyEventOnAggregate(@event);
        }

        private void ApplyEventOnAggregate<TEvent>(TEvent @event)
        {
            try
            {
                var method = GetType().GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(TEvent) }, null);
                method.Invoke(this, new[] { @event as Object });
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("DDD.Light.CQRS.InProcess.AggregateRoot -> ApplyEventOnAggregate: Failed to apply event on aggregate type: {0} through reflection. Event type {1} did not get applied", GetType(), typeof(TEvent)), ex);
            }
        }

    }
}