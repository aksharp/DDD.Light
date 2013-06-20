using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DDD.Light.EventStore.Contracts;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.EventStore
{
    public class EventStore : IEventStore
    {
        private static volatile EventStore _instance;
        private IRepository<AggregateEvent> _repo;
        private static object token = new Object();
        private IEventSerializationStrategy _serializationStrategy;

        public static IEventStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (token)
                    {
                        if (_instance == null)
                            _instance = new EventStore();
                    }
                }
                return _instance;
            }
        }

        public void Configure(IRepository<AggregateEvent> repository, IEventSerializationStrategy serializationStrategy)
        {
            _repo = repository;
            _serializationStrategy = serializationStrategy;
        }

        public IEnumerable<AggregateEvent> GetAll()
        {
            return _repo.GetAll();
        }

        public IEnumerable<AggregateEvent> GetAll(DateTime until)
        {
            return _repo.Get().Where(x => DateTime.Compare(x.CreatedOn, until) <= 0);
        }

        public long Count()
        {
            return _repo.Count();
        }

        private void VerifyRepoIsConfigured()
        {
            if (_repo == null) throw new Exception("Event Store Repository is not configured. Use EventStore.Instance.Configure(); to configure");
        }

        public T GetById<T>(Guid id) 
        {
            VerifyRepoIsConfigured();

            var constructors = (typeof(T)).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var aggregate = (T)constructors[0].Invoke(new object[] { });

            _repo.Get().Where(x => x.AggregateId == id).OrderBy(x => x.CreatedOn).ToList().ForEach(aggregateEvent =>
                {
                    var eventType = Type.GetType(aggregateEvent.EventType);
                    var @event = _serializationStrategy.DeserializeEvent(aggregateEvent.SerializedEvent, eventType);
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
                    var @event = _serializationStrategy.DeserializeEvent(aggregateEvent.SerializedEvent, eventType);
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
                var @event = _serializationStrategy.DeserializeEvent(aggregateEvent.SerializedEvent, eventType);
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