using System;

namespace DDD.Light.EventStore.Contracts
{
    public interface IEventSerializationStrategy
    {
        string SerializeEvent(object @event);
        object DeserializeEvent(string serializedEvent, Type eventType);
    }
}