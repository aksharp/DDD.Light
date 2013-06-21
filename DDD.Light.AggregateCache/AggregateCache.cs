using System;
using System.Reflection;
using DDD.Light.AggregateCache.Contracts;
using DDD.Light.CQRS.Contracts;
using DDD.Light.EventStore.Contracts;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.AggregateCache
{
    public class AggregateCache : IAggregateCache
    {
        private static volatile AggregateCache _instance;
        private static object token = new Object();
        private IEventStore _eventStore;

        private AggregateCache(){}

        public static IAggregateCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (token)
                    {
                        if (_instance == null)
                            _instance = new AggregateCache();
                    }
                }
                return _instance;
            }
        }

        private Func<Type, object> _getAggregateCacheRepositoryInstance;
        public void Configure(IEventStore eventStore, Func<Type, object> getAggregateCacheRepositoryInstance)
        {
            _eventStore = eventStore;
            _getAggregateCacheRepositoryInstance = getAggregateCacheRepositoryInstance;
        }

        private IRepository<T> GetRepository<T>()
        {
            return _getAggregateCacheRepositoryInstance(typeof(IRepository<T>)) as IRepository<T>;
        }

        public T GetById<T>(Guid id) where T : IAggregateRoot
        {
            var cachedAggregate = GetRepository<T>().GetById(id);
            if (Equals(cachedAggregate, default(T)))
            {
                var aggregate = _eventStore.GetById<T>(id);
                GetRepository<T>().Save(aggregate);
                return aggregate;
            }
            return cachedAggregate;
        }

        public void Handle<TAggregate, TEvent>(Guid aggregateId, TEvent @event) where TAggregate : IAggregateRoot
        {
            var aggregate = GetRepository<TAggregate>().GetById(aggregateId);
            if (Equals(aggregate, default(TAggregate)))
                aggregate = _eventStore.GetById<TAggregate>(aggregateId);
            if (!Equals(aggregate, default(TAggregate))) 
                ApplyEvent(@event, aggregate);
        }

        public void Clear(Guid aggregateId, Type aggregateType)
        {
            // todo: get repository of Type and delete by aggregateId
        }

        private static void ApplyEvent<TAggregate, TEvent>(TEvent @event, TAggregate aggregate) where TAggregate : IAggregateRoot
        {
            var eventType = typeof (TEvent);
            var method = typeof (TAggregate).GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] {eventType}, null);
            method.Invoke(aggregate, new[] {@event as Object});
        }
    }
}
