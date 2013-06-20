using System;
using System.Reflection;
using DDD.Light.AggregateCache.Contracts;
using DDD.Light.CQRS.Contracts;
using DDD.Light.EventStore.Contracts;

namespace DDD.Light.AggregateCache.InMemory
{
    public class InMemoryAggregateCache : IAggregateCache
    {
        private static volatile InMemoryAggregateCache _instance;
        private static object token = new Object();
        private IEventStore _eventStore;

        private InMemoryAggregateCache(){}

        public static IAggregateCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (token)
                    {
                        if (_instance == null)
                            _instance = new InMemoryAggregateCache();
                    }
                }
                return _instance;
            }
        }

        
        public void Configure(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public T GetById<T>(Guid id) where T : IAggregateRoot
        {
            var cachedAggregate = AggregateDatabase<T>.Instance.GetById(id);
            if (Equals(cachedAggregate, default(T)))
            {
                var aggregate = _eventStore.GetById<T>(id);
                AggregateDatabase<T>.Instance.Add(aggregate);
                return aggregate;
            }
            return cachedAggregate;
        }

        public void Handle<TAggregate, TEvent>(Guid aggregateId, TEvent @event) where TAggregate : IAggregateRoot
        {
            var aggregate = AggregateDatabase<TAggregate>.Instance.GetById(aggregateId);
            ApplyEvent(@event, aggregate);
        }

        private static void ApplyEvent<TAggregate, TEvent>(TEvent @event, TAggregate aggregate) where TAggregate : IAggregateRoot
        {
            var eventType = typeof (TEvent);
            var method = typeof (TAggregate).GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] {eventType}, null);
            method.Invoke(aggregate, new[] {@event as Object});
        }
    }
}
