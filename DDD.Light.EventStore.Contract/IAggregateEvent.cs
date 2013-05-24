using System;

namespace DDD.Light.EventStore.Contract
{
    public interface IAggregateEvent
    {
        Guid AggregateId { get; }
        DateTime CreatedOn { get; }
        Type EventType { get; }
        string Payload { get; }
    }

    public class AggregateEvent : IAggregateEvent
    {
        public AggregateEvent(Guid aggregateId, Type eventType, string payload)
        {
            AggregateId = aggregateId;
            EventType = eventType;
            Payload = payload;
            CreatedOn = DateTime.UtcNow;
        }

        public Guid AggregateId { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public Type EventType { get; private set; }
        public Type AggregateType { get; private set; }
        public string Payload { get; private set; }
    }
}
