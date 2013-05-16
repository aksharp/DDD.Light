using System;
using System.Collections.Generic;

namespace DDD.Light.Messaging
{
    public interface IEventHandlersDatabase<T>
    {
        void Add(IEventHandler<T> eventHandler);
        void Add(Action<T> eventHandler);
        IEnumerable<Action<T>> Get();
    }

}