using System;

namespace DDD.Light.Repo.Contracts
{
    public abstract class Entity
    {
        public Guid? Id { get; set; }
    }
}