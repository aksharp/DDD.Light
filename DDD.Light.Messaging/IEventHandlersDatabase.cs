using System.Collections.Generic;

namespace DDD.Light.Messaging
{
    public interface IEventHandlersDatabase<T>
    {
        void Add(IEventHandler<T> eventHandler);
        IEnumerable<IEventHandler<T>> Get();
    }
}