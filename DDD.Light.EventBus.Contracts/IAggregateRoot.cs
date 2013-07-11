namespace DDD.Light.EventBus.Contracts
{
    public interface IAggregateRoot<TId> : IEntity<TId>
    {
        void PublishEvent<TEvent>(TEvent @event);
        void PublishAndApplyEvent<TEvent>(TEvent @event);
    }
}
