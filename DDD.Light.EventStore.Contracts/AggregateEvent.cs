using System;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.EventStore.Contracts
{
//    public class AggregateEvent2<TAggregateEventId, TAggregateId> : IEntity<TAggregateEventId>
//    {
//        public TAggregateEventId Id { get; set; }
//        public string AggregateType { get; set; }
//        public TAggregateId AggregateId { get; set; }
//        public DateTime CreatedOn { get; set; }
//        public string EventType { get; set; }        
//        public string SerializedEvent { get; set; }
//    }
    
    public class AggregateEvent : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string AggregateType { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EventType { get; set; }        
        public string SerializedEvent { get; set; }
        public string AggregateIdType { get; set; }
        public string SerializedAggregateId { get; set; }
    }

//    public interface IAggregateEvent
//    {
//        void Configure<TAggregateEventId, TAggregateId>(TAggregateEventId aggregateEventId, TAggregateId aggregateId);
//    }
}