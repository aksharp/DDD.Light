using System;
using DDD.Light.EventStore.Contracts;
using Newtonsoft.Json;

namespace DDD.Light.EventStore.MongoDB
{
    public class JsonEventSerializerStrategy : IEventSerializerStrategy
    {
        public string SerializeEvent(object @event)
        {
            return JsonConvert.SerializeObject(@event);
        }

        public object DeserializeEvent(string serializedEvent, Type eventType)
        {
            return JsonConvert.DeserializeObject(serializedEvent, eventType);
        }
    }
}