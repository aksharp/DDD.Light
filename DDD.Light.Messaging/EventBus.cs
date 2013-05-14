using System;
using System.Collections.Generic;

namespace DDD.Light.Messaging
{
    public class EventBus : IEventBus
    {
        private static volatile IEventBus _instance;
        private readonly List<IEventHandler> _registeredHandlers;
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

        private EventBus()
        {
            _registeredHandlers = new List<IEventHandler>();
        }

        public void Subscribe(IEventHandler handler)
        {
            _registeredHandlers.Add(handler);
        }

        public void Publish<T>(T @event) 
        {
            _registeredHandlers.ForEach(h => h.Handle(@event));
        }
    }

}
