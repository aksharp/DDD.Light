using System;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Repo.MongoDB
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }       
    }
}