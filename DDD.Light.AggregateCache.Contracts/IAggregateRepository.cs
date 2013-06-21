using System;
using DDD.Light.CQRS.Contracts;

namespace DDD.Light.AggregateCache.Contracts
{
    public interface IAggregateRepository<T> where T : IAggregateRoot 
    {
        void Add(T aggregate);
        T GetById(Guid aggregateId);
        void Configure(Func<Type, object> getInstance);
    }

}