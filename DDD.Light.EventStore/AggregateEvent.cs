using System;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.EventStore
{
    public class AggregateEvent : IEntity
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string SerializedEvent { get; set; }
        public Type AggregateType { get; set; }
        public Type EventType { get; set; }        
    }
}