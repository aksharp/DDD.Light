using System;
using DDD.Light.Messaging;
using Newtonsoft.Json;

namespace DDD.Light.EventStore.Contract
{
    public interface IAggregateRepository<TAggregate>
    {
        TAggregate GetById(Guid Id);
        void Save(IAggregateEvent @event);
    }


    public class Person : IAggregate
    {
        public Guid Id { get; private set; }
    }

    public class ARRepo<Person> : IAggregateRepository<Person>
    {
        public Person GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Save(IAggregateEvent @event)
        {
            throw new NotImplementedException();
        }
    }

    public class EventHandler
    {
        public void Save(SomeEvent e)
        {
            var repo = new ARRepo<Person>();
            var ae = new AggregateEvent(e.Id, e.GetType(), JsonConvert.SerializeObject(e));
            repo.Save(ae);
        }


    }


    public class EventSubscribtion
    {
        public EventSubscribtion()
        {
            EventBus.Instance.Subscribe((IAggregateEvent e) =>
                {
                    var dbName = "EventStore";
                    var collectionName = e.AggregateType;
                });
        }
    }
}
