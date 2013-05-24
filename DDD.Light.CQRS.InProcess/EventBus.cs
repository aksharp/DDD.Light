using System;
using System.Linq;
using System.Reflection;
using DDD.Light.CQRS.Contracts;
using DDD.Light.EventStore.Contracts;
using Newtonsoft.Json;

namespace DDD.Light.CQRS.InProcess
{
    public class EventBus : IEventBus
    {
        private static volatile IEventBus _instance;
        private static object token = new Object();

        public static IEventBus Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (token)
                    {
                        if (_instance == null)
                            _instance = new EventBus();
                    }
                }
                return _instance;
            }
        }

        private readonly Guid _instanceID;
        private EventBus()
        {
            _instanceID = Guid.NewGuid();
        }
       
        public Guid GetUniqueInstanceID()
        {
            return _instanceID;
        }

        public void Subscribe<T>(IEventHandler<T> handler)
        {
            EventHandlersDatabase<T>.Instance.Add(handler);
        }

        public void Subscribe<T>(Action<T> handleMethod)
        {
            EventHandlersDatabase<T>.Instance.Add(handleMethod);
        }

        public void Publish<T>(Type aggregateType, Guid aggregateId, T @event)
        {
            StoreEvent(aggregateType, aggregateId, @event);
            HandleEvent(@event);
        }

        public void Publish<TAggregate, T>(Guid aggregateId, T @event)
        {
            StoreEvent(typeof(TAggregate), aggregateId, @event);
            HandleEvent(@event);
        }

        private void HandleEvent<T>(T @event)
        {
            if (!Equals(@event, default(T)))
                new Transaction<T>(@event, EventHandlersDatabase<T>.Instance.Get().ToList()).Commit();
        }

        private void StoreEvent<T>(Type aggregateType, Guid aggregateId, T @event)
        {
            if (_eventStore == null) throw new Exception("Event Store is not configured. Use 'EventBus.Instance.Configure(eventStore);' to configure it.");
            _eventStore.Save(new AggregateEvent
                {
                    Id = Guid.NewGuid(),
                    AggregateId = aggregateId,
                    AggregateType = aggregateType.AssemblyQualifiedName,
                    EventType = typeof(T).AssemblyQualifiedName,
                    CreatedOn = DateTime.UtcNow,
                    SerializedEvent = JsonConvert.SerializeObject(@event)
                });
        }

        private IEventStore _eventStore;
        public void Configure(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public IEventStore GetEventStore()
        {
            return _eventStore;
        }

        public void RestoreReadModel()
        {
            _eventStore.GetAll().ToList().ForEach(HandleRestoreReadModelEvent);
        }

        public void RestoreReadModel(DateTime until)
        {
            _eventStore.GetAll(until).ToList().ForEach(HandleRestoreReadModelEvent);
        }

        private void HandleRestoreReadModelEvent(AggregateEvent aggregateEvent)
        {
            var eventType = Type.GetType(aggregateEvent.EventType);
            var @event = JsonConvert.DeserializeObject(aggregateEvent.SerializedEvent, eventType);
            GetType().GetMethod("HandleEvent", BindingFlags.NonPublic | BindingFlags.Instance)
                     .MakeGenericMethod(eventType)
                     .Invoke(Instance, new[] {@event});
        }

    }
}
