namespace DDD.Light.EventStore.Contracts
{
    public interface IAggregate
    {
        void ApplyEvent<T>(T @event);
    }
}