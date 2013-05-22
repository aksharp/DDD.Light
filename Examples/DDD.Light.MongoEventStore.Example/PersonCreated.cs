using System;

namespace DDD.Light.MongoEventStore.Example
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