using System;
using System.Linq;

namespace DDD.Light.Messaging
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

        public void Subscribe<T>(Action<T> handler)
        {
            throw new NotImplementedException();
        }

        public void Publish<T>(T @event) 
        {
            if ( !Equals( @event, default(T) ) )
                new Transaction<T>(@event, EventHandlersDatabase<T>.Instance.Get().ToList()).Commit();
        }
    }
}
