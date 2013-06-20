using System;
using DDD.Light.CQRS.Contracts;

namespace DDD.Light.AggregateCache.Contracts
{
    public interface IAggregateDatabase<T> where T : IAggregateRoot 
    {
        void Add(T aggregate);
        T GetById(Guid aggregateId);
    }

}