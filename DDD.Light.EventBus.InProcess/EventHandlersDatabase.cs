using System;
using System.Collections.Generic;
using DDD.Light.EventBus.Contracts;

namespace DDD.Light.EventBus.InProcess
{
    public class EventHandlersDatabase<TEvent> : IEventHandlersDatabase<TEvent>
    {
        private static volatile IEventHandlersDatabase<TEvent> _instance;
        private static object token = new Object();
        private readonly List<Action<TEvent>> _registeredHandlerActions;

        public static IEventHandlersDatabase<TEvent> Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (token)
                    {
                        if (_instance == null)
                            _instance = new EventHandlersDatabase<TEvent>();
                    }
                }
                return _instance;
            }
        }

        private EventHandlersDatabase()
        {
            _registeredHandlerActions = new List<Action<TEvent>>();
            _instanceID = Guid.NewGuid();
        }

        private readonly Guid _instanceID;
        public Guid GetUniqueInstanceID()
        {
            return _instanceID;
        }

        public void Add(IEventHandler<TEvent> eventHandler)
        {
            _registeredHandlerActions.Add(eventHandler.Handle);
        }

        public void Add(Action<TEvent> eventHandlerAction)
        {
            _registeredHandlerActions.Add(eventHandlerAction);
        }

        public IEnumerable<Action<TEvent>> Get()
        {
            return _registeredHandlerActions;
        }

    }
}