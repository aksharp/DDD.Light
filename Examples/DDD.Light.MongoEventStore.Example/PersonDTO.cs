﻿using DDD.Light.Repo.MongoDB;

namespace DDD.Light.MongoEventStore.Example
{
    public class PersonDTO : Entity
    {
        public string Name { get; set; }
        public bool WasRenamed { get; set; }
    }
}