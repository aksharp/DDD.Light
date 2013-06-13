using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DDD.Light.EventStore.Contracts;
using DDD.Light.Repo.Contracts;
using DDD.Light.Repo.MongoDB;

namespace DDD.Light.EventStore.MongoDB
{
    public class MongoEventStore : IEventStore
    {
        private static volatile MongoEventStore _instance;
        private IRepository<AggregateEvent> _repo;
        private IEventStoreBus _bus;
        private static object token = new Object();
        private IEventSerializerStrategy _serializerStrategy;

        public static IEventStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (token)
                    {
                        if (_instance == null)
                            _instance = new MongoEventStore();
                    }
                }
                return _instance;
            }
        }

        public void Configure(IStorageConfigStrategy storageConfigStrategy, IEventSerializerStrategy serializerStrategy, IEventStoreBus bus)
        {
            _bus = bus;
            var config = storageConfigStrategy as MongoStorageConfigStrategy;
            if (config == null) throw new Exception("Invalid MongoStorageConfigStrategy");
            _repo = new MongoRepository<AggregateEvent>(config.ConnectionString, config.DatabaseName, config.CollectionName);
            _serializerStrategy = serializerStrategy;
        }

        public IEnumerable<AggregateEvent> GetAll()
        {
            return _repo.GetAll();
        }

        public IEnumerable<AggregateEvent> GetAll(DateTime until)
        {
            return _repo.Get().Where(x => DateTime.Compare(x.CreatedOn, until) <= 0);
        }

        public void Subscribe(IEntity aggregate)
        {
            _bus.Subscribe(aggregate);
        }

        public void SubscribeSince(IEntity aggregate, DateTime since)
        {
            VerifyRepoIsConfigured();

            _repo.Get().Where(x => x.AggregateId == aggregate.Id && DateTime.Compare(x.CreatedOn, since) >= 0).OrderBy(x => x.CreatedOn).ToList().ForEach(aggregateEvent =>
            {
                var eventType = Type.GetType(aggregateEvent.EventType);
                var @event = _serializerStrategy.DeserializeEvent(aggregateEvent.SerializedEvent, eventType);
                var method = aggregate.GetType().GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { eventType }, null);
                method.Invoke(aggregate, new[] { @event });
            });

            Subscribe(aggregate);
        }

        public void Publish<T>(Type aggregateType, Guid aggregateId, T @event)
        {
            _bus.Publish(aggregateType, aggregateId, @event);
        }

        public T GetSubscribedById<T>(Guid id) where T : IEntity
        {
            var aggregate = GetById<T>(id);
            _bus.Subscribe(aggregate);
            return aggregate;
        }

        public object GetSubscribedById(Guid id)
        {
            var aggregate = GetById(id) as IEntity;
            _bus.Subscribe(aggregate);
            return aggregate;
        }

        private void VerifyRepoIsConfigured()
        {
            if (_repo == null) throw new Exception("Mongo Event Store Repository is not configured. Use MongoEventStore.Instance.Configure(connectionString, databaseName, collectionName); to configure");
        }

        public T GetById<T>(Guid id) 
        {
            VerifyRepoIsConfigured();

            var constructors = (typeof(T)).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var aggregate = (T)constructors[0].Invoke(new object[] { });

            _repo.Get().Where(x => x.AggregateId == id).OrderBy(x => x.CreatedOn).ToList().ForEach(aggregateEvent =>
                {
                    var eventType = Type.GetType(aggregateEvent.EventType);
                    var @event = _serializerStrategy.DeserializeEvent(aggregateEvent.SerializedEvent, eventType);
                    var method = typeof(T).GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[]{eventType}, null);
                    method.Invoke(aggregate, new[] { @event });
                });
            return aggregate;
        }
        
        public T GetById<T>(Guid id, DateTime until) 
        {
            VerifyRepoIsConfigured();

            var constructors = (typeof(T)).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var aggregate = (T)constructors[0].Invoke(new object[] { });

            _repo.Get().Where(x => x.AggregateId == id && DateTime.Compare(x.CreatedOn, until) <= 0).OrderBy(x => x.CreatedOn).ToList().ForEach(aggregateEvent =>
                {
                    var eventType = Type.GetType(aggregateEvent.EventType);
                    var @event = _serializerStrategy.DeserializeEvent(aggregateEvent.SerializedEvent, eventType);
                    var method = typeof(T).GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[]{eventType}, null);
                    method.Invoke(aggregate, new[] { @event });
                });
            return aggregate;
        }

        public object GetById(Guid id)
        {
            VerifyRepoIsConfigured();

            if (!_repo.Get().Any(x => x.AggregateId == id)) return null;

            var serializedAggregateType = _repo.Get().First(x => x.AggregateId == id).AggregateType;

            var aggregateType = Type.GetType(serializedAggregateType);

            if (aggregateType == null) return null;

            var constructors = aggregateType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var aggregate = constructors[0].Invoke(new object[] { });

            _repo.Get().Where(x => x.AggregateId == id).OrderBy(x => x.CreatedOn).ToList().ForEach(aggregateEvent =>
            {
                var eventType = Type.GetType(aggregateEvent.EventType);
                var @event = _serializerStrategy.DeserializeEvent(aggregateEvent.SerializedEvent, eventType);
                var method = aggregateType.GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { eventType }, null);
                try
                {
                    method.Invoke(aggregate, new[] {@event});
                }
                catch (Exception ex)
                {
                    throw new Exception("Please check if ApplyEvent for eventTyoe: " + aggregateEvent.EventType + " defined on aggregate: " + serializedAggregateType, ex);
                }
            });
            return aggregate;
        }

        public void Save(AggregateEvent aggregateEvent)
        {
            VerifyRepoIsConfigured();
            _repo.Save(aggregateEvent);
        }
    }
}