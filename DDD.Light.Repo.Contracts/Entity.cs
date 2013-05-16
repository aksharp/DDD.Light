using System;

namespace DDD.Light.Repo.Contracts
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}