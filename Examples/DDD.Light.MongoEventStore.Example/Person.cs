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
            EventBus.Instance.Publish(Id, new PersonCreated(Id));
        }

        public void NameMe(string name)
        {
            if (string.IsNullOrEmpty(_name))
                EventBus.Instance.Publish(Id, new PersonNamed(Id, name));
            else
                EventBus.Instance.Publish(Id, new PersonRenamed(Id, name));
        }



        // Apply Events

        public void ApplyEvent(PersonCreated personCreated)
        {
            Id = personCreated.Id;
        }

        public void ApplyEvent(PersonNamed personNamed)
        {
            _name = personNamed.Name;
        }
        
        public void ApplyEvent(PersonRenamed personNamed)
        {
            _name = personNamed.Name;
        }

    }
}