using System;
using System.Collections.Generic;
using DDD.Light.AggregateBus.Contracts;
using DDD.Light.AggregateStore.Contracts;
using DDD.Light.CQRS.Contracts;

namespace DDD.Light.AggregateBus.InProcess
{
    public class AggregateBus : IAggregateBus
    {
        private static volatile IAggregateBus _instance;
        private static object token = new Object();
        private readonly List<IAggregateStore> _registeredAggregateStores;
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

        public void Configure(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private AggregateBus()
        {
            _registeredAggregateStores = new List<IAggregateStore>();
        }

        public void Subscribe(IAggregateStore aggregateStore)
        {
            _registeredAggregateStores.Add(aggregateStore);
        }
               
        public void Publish<TAggregate, TEvent>(Guid aggregateId, TEvent @event) where TAggregate : IAggregateRoot
        {
            if (_eventBus == null) throw new Exception("EventBus is not configured");
            _eventBus.Publish<TAggregate, TEvent>(aggregateId, @event);
            _registeredAggregateStores.ForEach(aggregateStore => aggregateStore.Handle<TAggregate, TEvent>(aggregateId, @event));
        }

    }
}
