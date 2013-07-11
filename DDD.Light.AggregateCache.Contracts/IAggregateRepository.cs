using System;
using DDD.Light.CQRS.Contracts;

namespace DDD.Light.AggregateCache.Contracts
{
    public interface IAggregateRepository<TId, TAggregate> where TAggregate : IAggregateRoot<TId> 
    {
        void Add(TAggregate aggregate);
        TAggregate GetById(TId aggregateId);
        void Configure(Func<Type, object> getInstance);
    }

}