using System;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.EventStore.Contracts
{
    public class AggregateEvent : IEntity
    {
        public Guid Id { get; set; }
        public string AggregateType { get; set; }        
        public Guid AggregateId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EventType { get; set; }        
        public string SerializedEvent { get; set; }
    }
}