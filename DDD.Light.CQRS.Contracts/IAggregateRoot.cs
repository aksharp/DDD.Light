using DDD.Light.Repo.Contracts;

namespace DDD.Light.CQRS.Contracts
{
    public interface IAggregateRoot : IEntity
    {
        void PublishAndApplyEvent<TEvent>(TEvent @event);
        void PublishAndApplyEvent<TAggregate, TEvent>(TEvent @event) where TAggregate : IAggregateRoot;
    }
}
