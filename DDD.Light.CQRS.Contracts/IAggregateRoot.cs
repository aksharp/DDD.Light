using DDD.Light.Repo.Contracts;

namespace DDD.Light.CQRS.Contracts
{
    public interface IAggregateRoot : IEntity
    {
        void Publish<TAggregate, TEvent>(TEvent @event) where TAggregate : IAggregateRoot;
        void PublishEvent<T>(T @event);
//        void PublishEvent<TEvent>(TEvent @event);
//        void PublishAndApplyEvent<TEvent>(TEvent @event);
    }
}
