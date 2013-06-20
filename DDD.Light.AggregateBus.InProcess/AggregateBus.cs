using System;
using System.Collections.Generic;
using DDD.Light.AggregateBus.Contracts;
using DDD.Light.AggregateCache.Contracts;
using DDD.Light.CQRS.Contracts;

namespace DDD.Light.AggregateBus.InProcess
{
    public class AggregateBus : IAggregateBus
    {
        private static volatile IAggregateBus _instance;
        private static object token = new Object();
        private readonly List<IAggregateCache> _registeredAggregateCaches;
        private IEventBus _eventBus;

        public static IAggregateBus Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (token)
                    {
                        if (_instance == null)
                            _instance = new AggregateBus();
                    }
                }
                return _instance;
            }
        }

        public void Configure(IEventBus eventBus, IAggregateCache aggregateCache)
        {
            _eventBus = eventBus;
            _registeredAggregateCaches.Add(aggregateCache);
        }

        private AggregateBus()
        {
            _registeredAggregateCaches = new List<IAggregateCache>();
        }
               
        public void Publish<TAggregate, TEvent>(Guid aggregateId, TEvent @event) where TAggregate : IAggregateRoot
        {
            if (_eventBus == null) throw new ApplicationException("AggregateBus -> Publish failed. EventBus is not configured");
            _eventBus.Publish<TAggregate, TEvent>(aggregateId, @event);
            _registeredAggregateCaches.ForEach(aggregateCache => aggregateCache.Handle<TAggregate, TEvent>(aggregateId, @event));
        }

    }
}
