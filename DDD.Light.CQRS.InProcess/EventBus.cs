using System;
using System.Linq;
using System.Reflection;
using DDD.Light.CQRS.Contracts;
using DDD.Light.EventStore.Contracts;

namespace DDD.Light.CQRS.InProcess
{
    public class EventBus : IEventBus
    {
        private static volatile IEventBus _instance;
        private static object token = new Object();
        private IEventStore _eventStore;
        private IEventSerializationStrategy _eventSerializationStrategy;

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

        private EventBus(){}

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

        public void Configure(IEventStore eventStore, IEventSerializationStrategy eventSerializationStrategy)
        {
            _eventStore = eventStore;
            _eventSerializationStrategy = eventSerializationStrategy;
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
                    SerializedEvent = _eventSerializationStrategy.SerializeEvent(@event)
                });
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
            var @event = _eventSerializationStrategy.DeserializeEvent(aggregateEvent.SerializedEvent, eventType);
            GetType().GetMethod("HandleEvent", BindingFlags.NonPublic | BindingFlags.Instance)
                     .MakeGenericMethod(eventType)
                     .Invoke(Instance, new[] {@event});
        }

    }
}
