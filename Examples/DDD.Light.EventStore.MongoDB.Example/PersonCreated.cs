using System;

namespace DDD.Light.EventStore.MongoDB.Example
{
    public class PersonCreated
    {
        public Guid Id { get; private set; }

        public PersonCreated(Guid id)
        {
            Id = id;
        }
    }
}