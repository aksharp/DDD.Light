using DDD.Light.EventBus.Contracts;

namespace DDD.Light.EventBus.InProcess
{
    public class Entity<TId> : IEntity<TId>
    {
        public TId Id { get; protected set; }

        public Entity(TId id)
        {
            Id = id;
        }
    }
}
