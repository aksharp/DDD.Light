using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DDD.Light.EventStore.Contracts;
using DDD.Light.Repo.Contracts;
using DDD.Light.Repo.MongoDB;
using Newtonsoft.Json;

namespace DDD.Light.EventStore.MongoDB
{
    public class MongoEventStore : IEventStore
    {
        private static volatile MongoEventStore _instance;
        private IRepository<AggregateEvent> _repo;
        private static object token = new Object();

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

        private readonly Guid _instanceID;
        private MongoEventStore()
        {
            _instanceID = Guid.NewGuid();
        }
       
        public Guid GetUniqueInstanceID()
        {
            return _instanceID;
        }

        public void Configure(string connectionString, string databaseName, string collectionName)
        {
            _repo = new MongoRepository<AggregateEvent>(connectionString, databaseName, collectionName);
        }

        public IEnumerable<AggregateEvent> GetAll()
        {
            return _repo.GetAll();
        }

        public IEnumerable<AggregateEvent> GetAll(DateTime until)
        {
            return _repo.Get().Where(x => DateTime.Compare(x.CreatedOn, until) <= 0);
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
                    var @event = JsonConvert.DeserializeObject(aggregateEvent.SerializedEvent, eventType);
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
                    var @event = JsonConvert.DeserializeObject(aggregateEvent.SerializedEvent, eventType);
                    var method = typeof(T).GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[]{eventType}, null);
                    method.Invoke(aggregate, new[] { @event });
                });
            return aggregate;
        }

        public object GetById(Guid id)
        {
            VerifyRepoIsConfigured();

            var aggregateType = Type.GetType(_repo.Get().First(x => x.AggregateId == id).AggregateType);

            var constructors = aggregateType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            var aggregate = constructors[0].Invoke(new object[] { });

            _repo.Get().Where(x => x.AggregateId == id).OrderBy(x => x.CreatedOn).ToList().ForEach(aggregateEvent =>
            {
                var eventType = Type.GetType(aggregateEvent.EventType);
                var @event = JsonConvert.DeserializeObject(aggregateEvent.SerializedEvent, eventType);
                var method = aggregateType.GetMethod("ApplyEvent", BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { eventType }, null);
                method.Invoke(aggregate, new[] { @event });
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