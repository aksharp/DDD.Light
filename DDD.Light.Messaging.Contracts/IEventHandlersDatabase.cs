using System;
using System.Collections.Generic;

namespace DDD.Light.Messaging.Contracts
{
    public interface IEventHandlersDatabase<T>
    {
        void Add(IEventHandler<T> eventHandler);
        void Add(Action<T> handleMethod);
        IEnumerable<Action<T>> Get();
    }

}