using System;
using System.Linq;
using DDD.Light.EventBus.Contracts;
using DDD.Light.EventStore.Contracts;

namespace DDD.Light.EventBus.InProcess
{
    public class EventBus : IEventBus
    {
        private static volatile IEventBus _instance;
        private static object token = new Object();
        private IEventStore _eventStore;
        private IEventSerializationStrategy _eventSerializationStrategy;
        private bool _checkLatestEventTimestampPriorToSavingToEventStore;

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

        public void Subscribe<TEvent>(IEventHandler<TEvent> handler)
        {
            EventHandlersDatabase<TEvent>.Instance.Add(handler);
        }

        public void Subscribe<TEvent>(Action<TEvent> handleMethod)
        {
            EventHandlersDatabase<TEvent>.Instance.Add(handleMethod);
        }

        public void Publish<TId, TEvent>(Type aggregateType, TId id, TEvent @event)
        {
            HandleEvent(@event);
            StoreEvent(aggregateType, id, @event);
        }

        public void Configure(IEventStore eventStore, IEventSerializationStrategy eventSerializationStrategy, bool checkLatestEventTimestampPriorToSavingToEventStore)
        {
            _eventStore = eventStore;
            _eventSerializationStrategy = eventSerializationStrategy;
            _checkLatestEventTimestampPriorToSavingToEventStore = checkLatestEventTimestampPriorToSavingToEventStore;
        }

        private void HandleEvent<TEvent>(TEvent @event)
        {
            try
            {
                if (!Equals(@event, default(TEvent)))
                    new Transaction<TEvent>(@event, EventHandlersDatabase<TEvent>.Instance.Get().ToList()).Commit();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Transaction<T>(@event, EventHandlersDatabase<T>.Instance.Get().ToList()).Commit() failed");
            }
        }

        private void StoreEvent<TId, TEvent>(Type aggregateType, TId aggregateId, TEvent @event)
        {
            if (_eventStore != null){
                _eventStore.Save(new AggregateEvent
                {
                    Id = Guid.NewGuid(),
                    AggregateType = aggregateType.AssemblyQualifiedName,
                    EventType = typeof(TEvent).AssemblyQualifiedName,
                    CreatedOn = DateTime.UtcNow,
                    SerializedEvent = _eventSerializationStrategy.SerializeEvent(@event),
                    SerializedAggregateId = _eventSerializationStrategy.SerializeEvent(aggregateId),
                    AggregateIdType = typeof(TId).AssemblyQualifiedName
                });
            }
        }

    }
}
