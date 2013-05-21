using System;
using System.Linq;
using DDD.Light.Repo.Contracts;
using DDD.Light.Repo.MongoDB;
using Newtonsoft.Json;

namespace DDD.Light.EventStore
{
    public static class MongoEventStore
    {
        private static readonly IRepository<AggregateEvent> Repo;

        static MongoEventStore()
        {
            Repo = new MongoRepository<AggregateEvent>("mongodb://localhost", "DDDLight", "EventStore");
        }

        public static T GetById<T>(Guid id) where T : IAggregate
        {
            var aggreage = Activator.CreateInstance<T>();
            Repo.Get().Where(x => x.AggregateId == id).OrderBy(x => x.CreatedOn).ToList().ForEach(aggregateEvent =>
                {
                    var @event = JsonConvert.DeserializeObject(aggregateEvent.SerializedEvent, aggregateEvent.EventType);
                    aggreage.ApplyEvent(@event);
                });
            return aggreage;
        }

        public static void Save(AggregateEvent aggregateEvent)
        {
            Repo.Save(aggregateEvent);
        }
    }
}