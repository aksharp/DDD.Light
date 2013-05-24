using System;

namespace DDD.Light.Realtor.Core.Domain
{
    public abstract class Entity
    {
        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
