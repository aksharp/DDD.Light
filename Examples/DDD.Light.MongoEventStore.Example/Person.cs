using System;
using DDD.Light.Messaging.InProcess;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.MongoEventStore.Example
{
    public class Person : Entity
    {
        private string _name;

        private Person()
        {
            
        }

        public Person(Guid id)
        {
            Id = id;
            // cleanest way to call publish
            EventBus.Instance.Publish(GetType(), Id, new PersonCreated(Id));
        }


        // Aggregate API, all public

        public void NameMe(string name)
        {
            if (string.IsNullOrEmpty(_name))
            {
                // can call publish this way too, generic way
                EventBus.Instance.Publish<Person, PersonNamed>(Id, new PersonNamed(Id, name));
            }
            else
                // yes, this will work too, non generic way
                EventBus.Instance.Publish(typeof(Person), Id, new PersonRenamed(Id, name));
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