using System;
using DDD.Light.CQRS.InProcess;

namespace DDD.Light.EventStore.MongoDB.Example
{
    public class Person : AggregateRoot
    {
        private string _name;

        private Person()
        {
            
        }

        public Person(Guid id)
        {
            Id = id;
            // cleanest way to call publish
            PublishEvent(new PersonCreated(Id));
//            EventBus.Instance.Publish(GetType(), Id, new PersonCreated(Id));
        }


        // Aggregate API, all public

        public void NameMe(string name)
        {
            if (string.IsNullOrEmpty(_name))
            {
                // can call publish this way too, generic way
                PublishEvent(new PersonNamed(Id, name));
//                EventBus.Instance.Publish<Person, PersonNamed>(Id, new PersonNamed(Id, name));
            }
            else
                // yes, this will work too, non generic way
                PublishEvent(new PersonRenamed(Id, name));
//                EventBus.Instance.Publish(typeof(Person), Id, new PersonRenamed(Id, name));
        }



        // Apply Events (For rebuilding aggregate) all private

        private void ApplyEvent(PersonCreated personCreated)
        {
            Id = personCreated.Id;
        }

        private void ApplyEvent(PersonNamed personNamed)
        {
            _name = personNamed.Name;
        }

        private void ApplyEvent(PersonRenamed personNamed)
        {
            _name = personNamed.Name;
        }

    }
}