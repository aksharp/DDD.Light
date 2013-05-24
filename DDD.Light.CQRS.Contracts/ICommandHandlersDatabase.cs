using System;
using System.Collections.Generic;

namespace DDD.Light.CQRS.Contracts
{
    public interface ICommandHandlersDatabase<T>
    {
        void Add(ICommandHandler<T> commandHandler);
        void Add(Action<T> handleMethod);
        IEnumerable<Action<T>> Get();
    }

}