using System;
using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.EventStore.Contracts
{
    public interface IEventStore
    {
        T GetById<T>(Guid id);
        T GetById<T>(Guid id, DateTime until);
        object GetById(Guid id);
        void Save(AggregateEvent aggregateEvent);
        void Configure(IRepository<AggregateEvent> repo, IEventSerializationStrategy serializationStrategy);
        IEnumerable<AggregateEvent> GetAll();
        IEnumerable<AggregateEvent> GetAll(DateTime until);
        long Count();
        DateTime LatestEventTimestamp(Guid aggregateId);
    }
}