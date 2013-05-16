using System;

namespace DDD.Light.Messaging
{
    public interface IEventBus
    {
        void Subscribe<T>(IEventHandler<T> handler);
        void Subscribe<T>(Action<T> handler);
        void Publish<T>(T @event);
    }
}