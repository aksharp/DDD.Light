using System;
using System.Collections.Generic;

namespace DDD.Light.Messaging.Contracts
{
    public interface ICommandHandlersDatabase<T>
    {
        void Add(ICommandHandler<T> commandHandler);
        void Add(Action<T> handleMethod);
        IEnumerable<Action<T>> Get();
    }

}