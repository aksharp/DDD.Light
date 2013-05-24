using System;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.CQRS.InProcess
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }

        protected Entity()
        {
            
        }

        protected Entity(Guid id)
        {
            Id = id;
        }

        protected void PublishEvent<TEvent>(TEvent @event)
        {
            EventBus.Instance.Publish(GetType(), Id, @event);
        }
    }
}