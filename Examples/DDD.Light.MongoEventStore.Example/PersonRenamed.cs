using System;

namespace DDD.Light.MongoEventStore.Example
{
    public class PersonRenamed
    {
        public Guid PersonId { get; private set; }
        public string Name { get; private set; }

        public PersonRenamed(Guid id, string name)
        {
            PersonId = id;
            Name = name;
        }
    }
}