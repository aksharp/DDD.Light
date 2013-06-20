using System;
using System.Collections.Generic;
using DDD.Light.EventStore.Contracts;

namespace DDD.Light.EventStore.InMemory
{
    public class InMemoryEventStore : IEventStore
    {
        private static List<AggregateEvent> _aggregateEvents = new List<AggregateEvent>();

        public T GetById<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(Guid id, DateTime until)
        {
            throw new NotImplementedException();
        }

        public object GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(AggregateEvent aggregateEvent)
        {
            _aggregateEvents.Add(aggregateEvent);
        }

        public void Configure(IStorageStrategy storageStrategy, IEventSerializationStrategy serializationStrategy)
        {
            //no op
        }

        public IEnumerable<AggregateEvent> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AggregateEvent> GetAll(DateTime until)
        {
            throw new NotImplementedException();
        }
    }
}
