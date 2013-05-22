using System;

namespace DDD.Light.MongoEventStore.Example
{
    public class PersonNamed
    {
        public Guid PersonId { get; private set; }
        public string Name { get; private set; }

        public PersonNamed(Guid personId, string name)
        {
            PersonId = personId;
            Name = name;
        }
    }
}