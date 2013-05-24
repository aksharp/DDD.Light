using System;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.ReadModel.DAO
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }       
    }
}