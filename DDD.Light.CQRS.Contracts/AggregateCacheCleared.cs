using System;

namespace DDD.Light.CQRS.Contracts
{
    public class AggregateCacheCleared
    {
        public Guid AggregateId { get; private set; }
        public Type AggregateType { get; private set; }

        public AggregateCacheCleared(Guid aggregateId, Type aggregateType)
        {
            AggregateId = aggregateId;
            AggregateType = aggregateType;
        }
    }
}