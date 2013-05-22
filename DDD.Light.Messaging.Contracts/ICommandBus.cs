using System;

namespace DDD.Light.Messaging.Contracts
{
    public interface ICommandBus
    {
        void Subscribe<T>(ICommandHandler<T> handler);
        void Subscribe<T>(Action<T> handler);
        void Dispatch<T>(T command);
    }
}