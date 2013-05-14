using System;
using System.Collections.Generic;

namespace DDD.Light.Messaging
{
    public class EventHandlersDatabase<T> : IEventHandlersDatabase<T>
    {
        private static volatile IEventHandlersDatabase<T> _instance;
        private static object token = new Object();
        private readonly List<IEventHandler<T>> _registeredHandlers;

        public static IEventHandlersDatabase<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (token)
                    {
                        if (_instance == null)
                            _instance = new EventHandlersDatabase<T>();
                    }
                }
                return _instance;
            }
        }

        private EventHandlersDatabase()
        {
            _registeredHandlers = new List<IEventHandler<T>>();
        }

        public void Add(IEventHandler<T> eventHandler)
        {
            _registeredHandlers.Add(eventHandler);
        }

        public IEnumerable<IEventHandler<T>> Get()
        {
            return _registeredHandlers;
        }

    }
}