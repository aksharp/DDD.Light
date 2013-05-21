using System;
using System.Linq;
using DDD.Light.EventStore;
using Newtonsoft.Json;

namespace DDD.Light.Messaging.InProcess
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
       
        public void Subscribe<T>(IEventHandler<T> handler)
        {
            EventHandlersDatabase<T>.Instance.Add(handler);
        }

        public void Subscribe<T>(Action<T> handleMethod)
        {
            EventHandlersDatabase<T>.Instance.Add(handleMethod);
        }

        public void Publish<T>(Guid aggregateId, T @event)
        {
            StoreEvent(aggregateId, @event);
            HandleEvent(@event);
        }

        private static void HandleEvent<T>(T @event)
        {
            if (!Equals(@event, default(T)))
                new Transaction<T>(@event, EventHandlersDatabase<T>.Instance.Get().ToList()).Commit();
        }

        private static void StoreEvent<T>(Guid aggregateId, T @event)
        {
            MongoEventStore.Save(new AggregateEvent
                {
                    Id = Guid.NewGuid(),
                    AggregateId = aggregateId,
                    EventType = typeof (T),
                    CreatedOn = DateTime.UtcNow,
                    SerializedEvent = JsonConvert.SerializeObject(@event)
                });
        }
    }
}
