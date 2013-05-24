using System;

namespace DDD.Light.EventStore.MongoDB.Example
{
    public class PersonRenamed
    {
        public Guid PersonId { get; private set; }
        public string Name { get; private set; }

        public PersonRenamed(Guid personId, string name)
        {
            PersonId = personId;
            Name = name;
        }
    }
}