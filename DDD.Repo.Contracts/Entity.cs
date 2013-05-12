using System;

namespace DDD.Repo.Contracts
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}