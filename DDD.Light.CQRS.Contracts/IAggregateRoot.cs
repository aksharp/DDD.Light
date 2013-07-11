using DDD.Light.Repo.Contracts;

namespace DDD.Light.CQRS.Contracts
{
    public interface IAggregateRoot<TId> : IEntity<TId>
    {
        void PublishAndApplyEvent<TEvent>(TEvent @event);
        void PublishAndApplyEvent<TAggregate, TEvent>(TEvent @event) where TAggregate : IAggregateRoot<TId>;
    }
}
