using System;

namespace DDD.Light.CQRS.Contracts
{
    public class AggregateCacheCleared
    {
        public string SerializedAggregateId { get; private set; }
        public Type AggregateIdType { get; private set; }
        public Type AggregateType { get; private set; }

        public AggregateCacheCleared(string serializedAggregateId, Type aggregateIdType, Type aggregateType)
        {
            SerializedAggregateId = serializedAggregateId;
            AggregateIdType = aggregateIdType;
            AggregateType = aggregateType;
        }
    }
}