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

        private IRepository<TId, TAggregate> GetRepository<TId, TAggregate>()
        {
            return _getAggregateCacheRepositoryInstance(typeof(IRepository<TId, TAggregate>)) as IRepository<TId, TAggregate>;
        }

        public TAggregate GetById<TId, TAggregate>(TId id) where TAggregate : IAggregateRoot<TId>
        {
            var cachedAggregate = GetRepository<TId, TAggregate>().GetById(id);
            if (Equals(cachedAggregate, default(TAggregate)))
            {
                var aggregate = _eventStore.GetById<TId, TAggregate>(id);
                GetRepository<TId, TAggregate>().Save(aggregate);
                return aggregate;
            }
            return cachedAggregate;
        }

        public void Handle<TAggregate, TId, TEvent>(TId aggregateId, TEvent @event) where TAggregate : IAggregateRoot<TId>
        {
            var aggregate = GetRepository<TId, TAggregate>().GetById(aggregateId);
            if (Equals(aggregate, default(TAggregate)))
                aggregate = _eventStore.GetById<TId, TAggregate>(aggregateId);
            if (!Equals(aggregate, default(TAggregate)))
                ApplyEvent<TAggregate, TId, TEvent>(@event, aggregate);
        }

        public void Clear<TId>(TId aggregateId, Type aggregateType)
        {           
            // todo: get repository of Type and delete by aggregateId
        }

        private static void ApplyEvent<TAggregate, TId, TEvent>(TEvent @event, TAggregate aggregate) where TAggregate : IAggregateRoot<TId>
        {
            var eventType = typeof (TEvent);
            var method = typeof (TAggregate).GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] {eventType}, null);
            method.Invoke(aggregate, new[] {@event as Object});
        }
    }
}
