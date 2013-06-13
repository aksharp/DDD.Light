using System;
using System.Collections.Generic;
using System.Reflection;
using DDD.Light.CQRS.Contracts;
using DDD.Light.EventStore.Contracts;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.EventStoreBus.InProcess
{
    public class EventStoreBus : IEventStoreBus
    {
        private static volatile IEventStoreBus _instance;
        private static object token = new Object();
        private readonly Dictionary<Guid, IEntity> _registeredAggregates;
        private IEventBus _eventBus;

        public static IEventStoreBus Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (token)
                    {
                        if (_instance == null)
                            _instance = new EventStoreBus();
                    }
                }
                return _instance;
            }
        }

        public void Configure(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private EventStoreBus()
        {
            _registeredAggregates = new Dictionary<Guid, IEntity>();
        }

        public void Subscribe(IEntity aggregate)
        {
            if (!_registeredAggregates.ContainsKey(aggregate.Id))
                _registeredAggregates.Add(aggregate.Id,aggregate);
        }

        public void Publish<T>(Type aggregateType, Guid aggregateId, T @event)
        {
            if (_eventBus == null) throw new Exception("EventBus is not configured");

            if (!_registeredAggregates.ContainsKey(aggregateId)) return;

            var subscribedAggregate = _registeredAggregates[aggregateId];
            var eventType = typeof(T);
            var method = subscribedAggregate.GetType().GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { eventType }, null);
            method.Invoke(this, new[] { @event as Object });

            _eventBus.Publish(aggregateType, aggregateId, @event);
        }

        public IEntity GetSubscribedAggregateById(Guid id)
        {
            return _registeredAggregates.ContainsKey(id) ? _registeredAggregates[id] : null;
        }
    }
}
